using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using KakeysBakeryClassLib.Components;

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
    public async void UpdateCurrentGood_SelectsCorrectGood()
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
        await unitUnderTest.UpdateCurrentGood(type.Id);

        // ASSERT
        Assert.Equal(type.Id, unitUnderTest.CurrentGoodTypeId);

        Assert.NotEmpty(unitUnderTest.CurrentDetails);
        Assert.Equal(2, unitUnderTest.CurrentDetails.Count);
        Assert.Equal(good2.Id, unitUnderTest.CurrentDetails.First().Id);
        Assert.Equal(good.Id, unitUnderTest.CurrentDetails.Last().Id);
    }

    [Fact]
    public async void CurrentGood_IsEmptyWhen_UpdatingGoodNotExists()
    {
        // ARRANGE
        CartManager unitUnderTest = new(client);

        // ACT
        await unitUnderTest.UpdateCurrentGood(9999);

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
        await unitUnderTest.UpdateCurrentGood(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}"
        });

        // ASSERT
        Assert.NotNull(unitUnderTest.Selected);
        Assert.Equal(good.Id, unitUnderTest.Selected.Id);
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
        await unitUnderTest.UpdateCurrentGood(type.Id);
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
        await unitUnderTest.UpdateCurrentGood(type.Id);
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
        await unitUnderTest.UpdateCurrentGood(type.Id);
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
        await unitUnderTest.UpdateCurrentGood(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}",
        });

        //await unitUnderTest.GetProduct();
        Assert.Fail();
    }

    [Fact]
    public async void GetProduct()
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
        await unitUnderTest.UpdateCurrentGood(type.Id);
        await unitUnderTest.UpdateSelection(new ChangeEventArgs()
        {
            Value = $"{flavor.Id}",
        });

        await client.GetFromJsonAsync<Basegoodtype>($"api/ProductAddonBasegood/get/{unitUnderTest.Selected!.Typeid}/{unitUnderTest.CurrentGoodTypeId}");
        Assert.Fail();
    }
}
