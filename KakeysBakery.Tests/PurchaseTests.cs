using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class PurchaseTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public PurchaseTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public void CanPassATest()
    {
        Assert.Equal(1, 1); 
    }

    [Fact]
    public async Task Get_PurchaseList()
    {
        // ARRANGE
        Purchase testPurchase = new()
        {
            Id = 245,
            Actualprice = (decimal)100.40
        };

        await client.PostAsJsonAsync("api/Purchase/add", testPurchase);

        // ACT
        List<Purchase>? result = await client.GetFromJsonAsync<List<Purchase>>("api/Purchase/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_Purchase_ById()
    {
        // ARRANGE
        Purchase testPurchase = new()
        {
            Id = 246,
            Actualprice = (decimal)100.40
        };

        await client.PostAsJsonAsync("api/Purchase/add", testPurchase);

        // ACT
        Purchase? result = await client.GetFromJsonAsync<Purchase>($"api/Purchase/get/{testPurchase.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testPurchase.Actualprice, result.Actualprice);
        Assert.Equal(testPurchase.Id, result.Id);
    }

    [Fact]
    public async Task Get_Purchase_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegood>($"api/Purchase/get/{-1}");
        });
    }

    [Fact]
    public async Task Create_Purchase()
    {
        // ARRANGE
        Purchase testPurchase = new()
        {
            Id = 248,
            Actualprice = (decimal)100.40
        };

        // ACT
        await client.PostAsJsonAsync("api/Purchase/add", testPurchase);
        Purchase? result = await client.GetFromJsonAsync<Purchase>($"api/Purchase/get/{testPurchase.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testPurchase.Actualprice, result.Actualprice);
        Assert.Equal(testPurchase.Id, result.Id);
    }

    [Fact]
    public async Task Edit_Purchase()
    {
        // ARRANGE
        Purchase testPurchase = new()
        {
            Id = 249,
            Actualprice = (decimal)100.40
        };

        await client.PostAsJsonAsync("api/Purchase/add", testPurchase);

        // ACT
        testPurchase.Actualprice = (decimal)123.50;
        await client.PatchAsJsonAsync("api/Purchase/update", testPurchase);
        
        Purchase? result = await client.GetFromJsonAsync<Purchase>($"api/Purchase/get/{testPurchase.Id}");


        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testPurchase.Actualprice, result.Actualprice);
        Assert.Equal(testPurchase.Id, result.Id);
    }

    [Fact]
    public async Task Delete_Purchase()
    {
        // ARRANGE
        Purchase testPurchase = new()
        {
            Id = 250,
            Actualprice = (decimal)100.40
        };

        await client.PostAsJsonAsync("api/Purchase/add", testPurchase);

        // ACT
        await client.DeleteAsync($"api/Purchase/delete/{testPurchase.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addon>($"api/Purchase/get/{testPurchase.Id}");
        });
    }

    [Fact]
    public async Task Delete_Purchase_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/Purchase/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}