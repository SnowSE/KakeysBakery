using MimeKit;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace KakeysBakery.Components.PayPalAuth;


public class PayPalAuthentication(HttpClient client, IConfiguration config) : IPayPalAuthentication
{
    const string baseurl = "https://api-m.sandbox.paypal.com/";
    ///node.js form 

    public async Task<string> GetAuthToken()
    {
        
        var fullUrl = $"{baseurl}v1/oauth2/token";
        Auth authorization = new Auth()
        {
            username = config["Test_Client_id"],
            password = config["Test_Client_secret"],
        };
        AuthToken token = new AuthToken()
        {
            data = "grant_type=client_credentials",
            auth = authorization
        };

        var json = JsonSerializer.Serialize(token);

        var response = await client.PostAsJsonAsync(fullUrl, json);

        return await response.Content.ReadAsStringAsync();

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
        //return null;
    }

    public async void CreateOrder(decimal purchaseAmt)
    {
        var accessToken = await GetAuthToken();
        var fullUrl = $"{baseurl}v2/checkout/orders";

        Headers header = new Headers()
        {
            Content_type = "application/json",
            Authorization = $"Bearer {accessToken}"
        };
        Amount amount = new Amount()
        {
            currency_code = "USD",
            value = purchaseAmt
        };
        Purchase_Units purchase_Units = new Purchase_Units()
        {
            amount = amount
        };
        Body body = new Body()
        {
            intent = "CAPTURE",
            purchase_units = [purchase_Units],
        };

        PaymentOrder baseJSON = new PaymentOrder()
        {
            method = "POST",
            headers = header,
            body = body
        };

        var json = JsonSerializer.Serialize(baseJSON);

        var response = await client.PostAsJsonAsync(fullUrl, json);
        
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

    public async void CapturePayment(int orderid)
    {
        var accesstoken = await GetAuthToken();
        var fullUrl = $"{baseurl}v2/checkout/orders/{orderid}/capture";

        Headers header = new Headers()
        {
            Content_type = "application/json",
            Authorization = $"Bearer {accesstoken}"
        };
        PaymentOrder baseJSON = new PaymentOrder()
        {
            method = "POST",
            headers = header
        };

        var json = JsonSerializer.Serialize(baseJSON);

        var response = await client.PostAsJsonAsync(fullUrl, json);
        
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
