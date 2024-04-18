//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MauiKakeys.MauiAuth0;
//public class Auth0ClientOptions
//{
//    public Auth0ClientOptions()
//    {
//        Scope = "openid";
//        RedirectUri = "myapp://callback";
//        Browser = new WebBrowserAuthenticator();
//        Domain = "dev-zas6rizyxopiwv2b.us.auth0.com";
//        ClientId = "xUuj4xt0Pn4wLdompKNjM3suZZKx9fdC";
//    }

//    public string Domain { get; set; }

//    public string ClientId { get; set; }

//    public string RedirectUri { get; set; }

//    public string Scope { get; set; }

//    public IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; }
//}