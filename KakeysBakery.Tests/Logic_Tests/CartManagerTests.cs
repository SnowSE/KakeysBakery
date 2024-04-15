using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

using KakeysSharedLib.Components;

using Microsoft.AspNetCore.Components;

namespace KakeysBakeryTests.Logic_Tests;

public class CartManagerTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public CartManagerTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public async void SelectGoodTypeCard_SelectsCorrectGood()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 95
        };

        Basegoodtype type = new()
        {
            Id = 96
        };

        Basegood good = new()
        {
            Id = 97,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        Basegood good2 = new()
        {
            Id = 98,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);
        await client.PostAsJsonAsync("api/basegood/add", good2);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);

        // ASSERT
        Assert.Equal(type.Id, unitUnderTest.CurrentGoodTypeId);

        Assert.NotEmpty(unitUnderTest.CurrentDetails);
        Assert.Equal(2, unitUnderTest.CurrentDetails.Count);
        Assert.Equal(good2.Id, unitUnderTest.CurrentDetails.First().Id);
        Assert.Equal(good.Id, unitUnderTest.CurrentDetails.Last().Id);
    }

    [Fact]
    public async void CurrentGood_IsEmptyWhen_SelectGoodTypeCardArguents_NotExists()
    {
        // ARRANGE
        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(9999);

        // ASSERT
        Assert.Equal(9999, unitUnderTest.CurrentGoodTypeId); // Should we keep the currentId if it's not in the database?
        Assert.Empty(unitUnderTest.CurrentDetails);
    }

    [Fact]
    public async void UpdateSelection_ChangesCurrentGood()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 95
        };

        Basegoodtype type = new()
        {
            Id = 96
        };

        Basegood good = new()
        {
            Id = 97,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}"
        });

        // ASSERT
        Assert.NotNull(unitUnderTest.Selected);
        Assert.Equal(good.Id, unitUnderTest.Selected.Id);
        Assert.Equal(flavor.Id, unitUnderTest.Selected.Flavorid);
        Assert.Equal(type.Id, unitUnderTest.Selected.Typeid);
    }

    [Fact]
    public async void UpdateSelection_DoesNotChangeGood_When_ChangeArgsAreNull()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 95
        };

        Basegoodtype type = new()
        {
            Id = 96
        };

        Basegood good = new()
        {
            Id = 97,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = null,
        });

        // ASSERT
        Assert.Null(unitUnderTest.Selected);
    }

    [Fact]
    public async void UpdateSelection_DoesNotChangeGood_When_ChangeArgsAreDefault()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 95
        };

        Basegoodtype type = new()
        {
            Id = 96
        };

        Basegood good = new()
        {
            Id = 97,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = "---",
        });

        // ASSERT
        Assert.Null(unitUnderTest.Selected);
    }

    [Fact]
    public async void UpdateSelection_DoesNotChangeGood_When_SelectionNotInDatabase()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 95
        };

        Basegoodtype type = new()
        {
            Id = 96
        };

        Basegood good = new()
        {
            Id = 97,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = "9999",
        });

        // ASSERT
        Assert.Null(unitUnderTest.Selected);
    }

    [Fact]
    private async void AddToCart()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 9500
        };

        Basegoodtype type = new()
        {
            Id = 9600
        };

        Basegood good = new()
        {
            Id = 9700,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        Product prod = new()
        {
            Id = 9800
        };

        ProductAddonBasegood pab = new()
        {
            Id = 1102,
            Basegoodid = good.Id,
            Productid = prod.Id,
        };

        Customer customer = new()
        {
            Email = "test@example.com"
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);
        await client.PostAsJsonAsync("api/product/add", prod);
        await client.PostAsJsonAsync("api/productAddonBasegood/add", pab);
        await client.PostAsJsonAsync("api/customer/add", customer);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}",
        });

        var result = await unitUnderTest.AddToCart(customer.Email);

        // ASSERT
    }

    [Fact]
    public async void GetProduct_WhenExists()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 9500
        };

        Basegoodtype type = new()
        {
            Id = 9600
        };

        Basegood good = new()
        {
            Id = 9700,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        Product product = new()
        {
            Id = 9800
        };

        ProductAddonBasegood pab = new()
        {
            Id = 1102,
            Basegoodid = good.Id,
            Productid = product.Id,
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);
        await client.PostAsJsonAsync("api/product/add", product);
        await client.PostAsJsonAsync("api/productAddonBasegood/add", pab);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}",
        });

        var result = await unitUnderTest.GetProduct();

        // ASSERT
        Assert.NotNull(result);
    }

    [Fact]
    public async void GetProduct_CreatesProduct_WhenNotExists()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 9501,
            Flavorname = "Yumm2"
        };

        Basegoodtype type = new()
        {
            Id = 9602,
            Basegood = "Mweep2"
        };

        Basegood good = new()
        {
            Id = 9703,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        ProductAddonBasegood pab = new()
        {
            Id = 1104,
            Basegoodid = good.Id,
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);
        await client.PostAsJsonAsync("api/productAddonBasegood/add", pab);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}",
        });

        var result = await unitUnderTest.GetProduct();

        // ASSERT
        Assert.NotNull(result);
    }

    [Fact]
    public async void CreateProduct()
    {
        // ARRANGE
        Basegoodflavor flavor = new()
        {
            Id = 9505,
            Flavorname = "yummy"
        };

        Basegoodtype type = new()
        {
            Id = 9606,
            Basegood = "Mweep"
        };

        Basegood good = new()
        {
            Id = 9707,
            Typeid = type.Id,
            Flavorid = flavor.Id
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegoodflavor/add", flavor);
        await client.PostAsJsonAsync("api/basegood/add", good);

        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.SelectGoodTypeCard(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}",
        });

        var product = await unitUnderTest.CreateProduct();

        // ASSERT
        Assert.NotNull(product);
        Assert.Equal(true, product.Ispublic);

        var pab = await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get_by_productId/{product.Id}");
        Assert.NotNull(pab);
        Assert.Equal(good.Id, pab.Basegoodid);
    }
}