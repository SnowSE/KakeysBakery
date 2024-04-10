﻿using KakeysBakery.Data;

using Microsoft.AspNetCore.Mvc;

using PayPal.Api;

namespace KakeysBakery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context, IConfiguration iconfiguration)
        {
            _logger = logger;
            httpContextAccessor = context;
            _configuration = iconfiguration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(string? Cancel = null, string blogId = "", string PayerID = "", string guid = "")
        {
            //getting the apiContext  
            var ClientID = _configuration.GetValue<string>("PayPalKey");
            var ClientSecret = _configuration.GetValue<string>("PayPalSecret");
            var mode = _configuration.GetValue<string>("PayPalmode");
            APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID!, ClientSecret!, mode!);
            // apiContext.AccessToken="Bearer access_token$production$j27yms5fthzx9vzm$c123e8e154c510d70ad20e396dd28287";
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = PayerID;
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Scheme + "://" + this.Request.Host + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guidd = Convert.ToString((new Random()).Next(100000));
                    guid = guidd;
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string? paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;

                            //try this 
                            httpContextAccessor.HttpContext!.Session.SetString("payment", createdPayment.id);
                            // save the desired redirect URL for confirmation
                            var confirmationRedirectUrl = "https://localhost:7196/Confirmation"; // Change this to your desired confirmation URL
                            httpContextAccessor.HttpContext.Session.SetString("confirmationRedirectUrl", confirmationRedirectUrl);
                            // redirect the user to the PayPal approval URL
                            return Redirect(paypalRedirectUrl);
                        }
                    }
                    // saving the paymentID in the key guid  
                    httpContextAccessor.HttpContext!.Session.SetString("payment", createdPayment.id);
                    return Redirect(paypalRedirectUrl!);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  

                    var paymentId = httpContextAccessor.HttpContext!.Session.GetString("payment");
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId! as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {

                        return View("PaymentFailed");
                    }
                    var confirmationRedirectUrl = httpContextAccessor.HttpContext.Session.GetString("confirmationRedirectUrl");
                    return Redirect(confirmationRedirectUrl!);
                }
            }
            catch (Exception)
            {
                return View("PaymentFailed");
            }
            //on successful payment, show success page to user.  
            //return View("SuccessView");
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            Payment payment = new Payment()
            {
                id = paymentId
            };
            return payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
        {
            //create itemlist and add item objects to it  

            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Detail",
                currency = "USD",
                price = "100.00",
                quantity = "1",
                sku = "asd"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = "https://localhost:7196/",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            //var details = new Details()
            //{
            //    tax = "1",
            //    shipping = "1",
            //    subtotal = "1"
            //};
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "100.00", // Total must be equal to sum of tax, shipping and subtotal.  
                                  //details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            Payment payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return payment.Create(apiContext);
        }


    }
}