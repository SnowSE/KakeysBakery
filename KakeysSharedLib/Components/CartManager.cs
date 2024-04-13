using System.Net.Http.Json;
using KakeysSharedLib.Data;
using Microsoft.AspNetCore.Components;

namespace KakeysSharedLib.Components;

public class CartManager(HttpClient client)
{
    public int CurrentGoodTypeId { get; set; }
    public List<Basegood> CurrentDetails { get; set; } = new();
    public Basegood? Selected { get; set; }
    public int Quantity { get; set; } = 1;
    public List<Basegoodtype> AvailableGoodTypes = new();

    public async Task SelectGoodTypeCard(int typeId)
    {
        CurrentGoodTypeId = typeId;

        try
        {
            CurrentDetails = await client.GetFromJsonAsync<List<Basegood>>($"api/Basegood/get_from_type/{CurrentGoodTypeId}") ?? new();
        }
        catch (System.Text.Json.JsonException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task UpdateSelection(ChangeEventArgs e)
    {
        if (e.Value is null) { return; }
        if (e.Value is "---") { return; }

        int flavorId = Convert.ToInt32(e.Value.ToString());

        try
        {
            Selected = await client.GetFromJsonAsync<Basegood>($"api/Basegood/get_from_flavor/{CurrentGoodTypeId}/{flavorId}");
        }
        catch (System.Text.Json.JsonException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<Product> GetProduct()
    {
        try
        {
            var result = await client.GetFromJsonAsync<ProductAddonBasegood>($"api/ProductAddonBasegood/get/{Selected!.Flavorid}/{CurrentGoodTypeId}");
            return await client.GetFromJsonAsync<Product>($"api/product/get/{result!.Productid}") ?? throw new NullReferenceException();
        }
        catch (HttpRequestException)
        {
            return await CreateProduct();
        }

    }

    public async Task<Product> CreateProduct()
    {
        var type = await client.GetFromJsonAsync<Basegoodtype>($"api/Basegoodtype/get/{Selected!.Typeid}");
        var flavor = await client.GetFromJsonAsync<Basegoodflavor>($"api/Basegoodflavor/get/{Selected!.Flavorid}");

        Product? product = new()
        {
            Ispublic = true,
            Productname = $"{type!.Basegood} {flavor!.Flavorname}"
        };

        await client.PostAsJsonAsync($"api/product/add", product);
        product = await client.GetFromJsonAsync<Product>($"api/product/get_by_name/{product.Productname}");

        ProductAddonBasegood pab = new()
        {
            Basegoodid = Selected!.Id,
            Productid = product!.Id
        };

        await client.PostAsJsonAsync($"api/productAddonBasegood/add", pab);

        return product;
    }

    public async Task<Cart> AddToCart(string email)
    {
        Product? product = await GetProduct() ??
            throw new NullReferenceException("No product found!");

        Customer? customer = await client.GetFromJsonAsync<Customer>($"api/Customer/get_by_email/{email}") ??
            throw new NullReferenceException("Customer not found in database!");

        Cart c = new()
        {
            Customerid = customer.Id,
            Productid = product.Id,
            Quantity = Quantity
        };

        await client.PostAsJsonAsync($"api/cart/add", c);
        var test = await client.GetFromJsonAsync<List<Cart>>($"api/Customer/getall");

        return await client.GetFromJsonAsync<Cart>($"api/cart/get_from_email/{email}") ??
            throw new NullReferenceException($"Unsuccessful in adding {Quantity} of {product.Productname} to cart for user {email}:");
    }

    public async Task PopulateAvailableGoodTypes()
    {
        AvailableGoodTypes = await client.GetFromJsonAsync<List<Basegoodtype>>($"api/Basegoodtype/getall") ?? [];
    }
}