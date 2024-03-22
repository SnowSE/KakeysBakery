namespace KakeysBakery.Components.PayPalAuth;

public class AuthToken
{
    public string body { get; set; }
    public Headers headers { get; set; }
    public string data { get; set; }

}

public class Auth
{
    public string username { get; set; }
    public string password { get; set; }
}
