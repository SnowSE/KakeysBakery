namespace KakeysBakery.Components.PayPalAuth;

public interface IPayPalAuthentication
{
    public  Task<string> GetAuthToken();
    public  void CreateOrder(decimal purchaseAmt);
    public  void CapturePayment(int orderid);
}
