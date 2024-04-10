using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace KakeysBakeryClassLib.PayPalAuth;

public class PayPalAuthentication(HttpClient client, IConfiguration config) : IPayPalAuthentication
{
    const string baseurl = "https://api-m.sandbox.paypal.com/";

    public async Task<string> GetAuthToken()
    {
        var fullUrl = $"{baseurl}v1/oauth2/token";

        var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
        var secrets = config["Test_Client_id"] + ":" + config["Test_Client_secret"];

        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(secrets)));

        request.Content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") });

        var response = await client.SendAsync(request);

        var authinfo = await response.Content.ReadFromJsonAsync<AuthorizationResponse>();
        return authinfo!.access_token!;
    }

    public class AuthorizationResponse
    {
        public string? scope { get; set; }
        public string? access_token { get; set; }
        public string? token_type { get; set; }
        public string? app_id { get; set; }
        public int expires_in { get; set; }
        public string? nonce { get; set; }
    }


    public async Task<string> CreateOrder(decimal purchaseAmt)
    {
        var accessToken = await GetAuthToken();
        var fullUrl = $"{baseurl}v2/checkout/orders";

        Amount saleamount = new Amount()
        {
            currency_code = "USD",
            value = purchaseAmt
        };
        Purchase_Units purchase_Units = new Purchase_Units()
        {
            amount = saleamount
        };
        OrderBody body = new OrderBody()
        {
            intent = "CAPTURE",
            purchase_units = [purchase_Units],
        };

        var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = new StringContent(JsonSerializer.Serialize(body));
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.SendAsync(request);

        var authinfo = await response.Content.ReadFromJsonAsync<OrderId>();

        return authinfo!.ID;
    }

    public class OrderId
    {
        public string ID { get; set; } = "";
    }

    public class OrderBody
    {
        public string intent { get; set; } = "";
        public Purchase_Units[] purchase_units { get; set; } = [];
    }

    public class Purchase_Units
    {
        public Amount? amount { get; set; }
    }

    public class Amount
    {
        public string currency_code { get; set; } = "";
        public decimal value { get; set; }
    }

    public async void CapturePayment(string orderid)
    {
        var accessToken = await GetAuthToken();
        var fullUrl = $"{baseurl}v2/checkout/orders/{orderid}/capture";

        var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = new StringContent("");
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.SendAsync(request);

        var authinfo = await response.Content.ReadAsStringAsync();

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