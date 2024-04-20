namespace KakeysBakery.Components.PayPalAuth;

public interface IPayPalAuthentication
{
    public Task<string> GetAuthToken();
    public Task<string> CreateOrder(decimal purchaseAmt);
    public void CapturePayment(string orderid);
}