using PayPal.Api;

namespace KakeysBakery.Data
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key  

        //Constructor  

        static PaypalConfiguration()
        {

        }
        // getting properties from the web.config  
        public static Dictionary<string, string> GetConfig(string mode)
        {
            return new Dictionary<string, string>()
{
{"mode",mode}
};
        }
        private static string GetAccessToken(string ClientId, string ClientSecret, string mode)
        {
            // getting accesstocken from paypal  
            string accessToken = new OAuthTokenCredential("AQtwU8oAij50ZxqcGY8QW929UjZ1swQEG3nBrk4lehIjlpnnUdGRhxdAeEFAyrsrlYUJIKzpZHTmjFLr", "ED356bHpLQolGcFaeAstDicQ4Rx1X1wqgaCYml811zCHq6IiPWO7rmWg8sG5LTg_EA4-FgYydDA0HkWX", new Dictionary<string, string>()
{
{"mode",mode}
}).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext(string clientId, string clientSecret, string mode)
        {
            // return apicontext object by invoking it with the accesstoken  
            APIContext apiContext = new APIContext(GetAccessToken(clientId, clientSecret, mode));
            apiContext.Config = GetConfig(mode);
            return apiContext;
        }
    }
}