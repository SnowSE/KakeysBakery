using AngleSharp.Diffing.Strategies.AttributeStrategies;
using KakeysBakery.Components;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace KakeysBakery.Services.nonDBServices;
public class CartLogic
{
    HttpClient client;
    public CartLogic(HttpClient Client)
    {
        client = Client;
    }
    public async Task<Product> addBaseGoodAsync(Basegood addBaseGood, Customer customer)
    {
        if (await baseGoodExistsAsync(addBaseGood.Id) == false) throw new InputFormatterException();
        await client.PostAsJsonAsync("api/Customer/add", customer);
        await client.PostAsJsonAsync("api/Basegood/add", addBaseGood);
        //await client.PostAsJsonAsync("api/Product/add", testProduct);
        //await client.PostAsJsonAsync("api/ProductAddonBasegood/add", finishedProduct);

        return new Product();
    }

    public async Task<int> FindProductForSingleBaseGoodAsync(int basegoodId)
    {
        List<ProductAddonBasegood> result = await client.GetFromJsonAsync<List<ProductAddonBasegood>>("api/productAddonBasegood/getall");
        
        //where there is a line that exists without an addon
        ProductAddonBasegood? result2 = result
            .Where(c => c.Basegoodid == basegoodId)
            .Where(c => c.Addonid is null).FirstOrDefault();
        
        //all the lines with addons and the same basegood
        List<ProductAddonBasegood> result3
             = result
            .Where(c => c.Basegoodid == basegoodId)
            .Where(c => c.Addonid is not null).ToList();

        //if this basegood is the only thing attached to the product
        if (result2 is not null && result3.Count == 0)
        { return (int)result2.Productid!; }

        //if the product is not connected, connect it
        else
        {
            Product product = new Product();
            await client.PostAsJsonAsync("api/product/add", product);
            ProductAddonBasegood productAddon = new ProductAddonBasegood()
            {
                Basegoodid = basegoodId,
                Productid = product.Id,
            };
            await client.PostAsJsonAsync("api/productAddonBasegood/add", productAddon);

            return product.Id;
        }
    }

    private async Task<bool> baseGoodExistsAsync(int basegoodId)
    {
        Basegood? result = null;
        try
        {
            result  = await client.GetFromJsonAsync<Basegood>($"api/basegood/get/{basegoodId}");
        }
        catch { return false; }
        if (result == null) return false;
        else return true;
    }
}
