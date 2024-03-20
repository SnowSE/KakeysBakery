using MimeKit;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace KakeysBakery;


public class PayPalAuth (HttpClient client)
{
   const string baseurl = "https://api-m.sandbox.paypal.com";
    ///node.js form 
    
    public async Task<string> GetAuthToken()
    {
        Auth authorization = new Auth()
        {
            username = "Client_id",
            password = "app_secret",
        };
        AuthToken token = new AuthToken()
        {
            url = baseurl + "v1/oauth2/token",
            data = "grant_type=client_credentials",
            auth = authorization
        };

        var serialized = JsonSerializer.Serialize(token);
        /*
        const response = await axios(
        {
            url :  "v1/oauth2/token",
            data : "grant_type=client_credentials",
        auth:{
            'username' : 'Client_id',
            'password' : 'app_secret',
        },
        });
         return response.data
         */
        return null;
    }

    public async void CreateOrder(int purchaseAmt)
    {
        var accessToken = await GetAuthToken();
        var fullUrl = $"{baseurl}/v2/checkout/orders";

        Headers header = new Headers()
        {
            Content_type = "application/json",
            Authorization = $"Bearer {accessToken}"
        };
        Amount amount = new Amount()
        {
            currency_code="USD",
            value = purchaseAmt
        };
        Purchase_Units purchase_Units = new Purchase_Units()
        {
            amount =amount
        };
        Body body = new Body()
        {
            intent = "CAPTURE",
            purchase_units = [purchase_Units],
        };

        CreateOrderJson baseJSON = new CreateOrderJson()
        {
            method = "POST",
            headers = header,
            body = body
        };

        var serialized = JsonSerializer.Serialize(baseJSON);

        /*
        await fetch (url,
        {
            method: "post",
            headers:{
               "Content-type": "application/json",
               "Authorization": "Bearer ${accesstoken}",
            },
            body: {
                "intent": "CAPTURE",
                "purchase_units":
               [
                    {
                        "amount": {
                           "currency_code": "USD", 
                            "value" : "purchaseAmt",
                        },
                    },
                ],
            },
        }
        );}
        */
    }

    public  async void CapturePayment(int orderid) {
        var accesstoken = await GetAuthToken();
        var fullurl = $"{baseurl}/v2/checkout/orders/{orderid}/capture";

        Headers header = new Headers()
        {
            Content_type= "application/json",
            Authorization= $"Bearer {accesstoken}"
        };
        CreateOrderJson baseJSON = new CreateOrderJson()
        {
            method = "POST",
            headers = header
        };
        /*
        await fetch (url, {
            method: "post",
            headers:{
               "Content-type": "application/json,
               Authorization: Bearer ${accesstoken},
            },
        });
         */
    }


}
