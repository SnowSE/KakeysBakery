namespace KakeysBakery.Components.PayPalAuth;

public class PaymentOrder
{
    public string method { get; set; }
    public Headers headers { get; set; }
    public Body body { get; set; }
}

public class Headers
{
    public string Content_type { get; set; }
    public string Authorization { get; set; }
}

public class Body
{
    public string intent { get; set; }
    public Purchase_Units[] purchase_units { get; set; }
}

public class Purchase_Units
{
    public Amount amount { get; set; }
}

public class Amount
{
    public string currency_code { get; set; }
    public decimal value { get; set; }
}
