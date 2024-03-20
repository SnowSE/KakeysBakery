namespace KakeysBakery;

public class AuthToken
{
    public string url { get; set; }
    public string data { get; set; }
    public Auth auth { get; set; }
}

public class Auth
{
    public string username { get; set; }
    public string password { get; set; }
}
