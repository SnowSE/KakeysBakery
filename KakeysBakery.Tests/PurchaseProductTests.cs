using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class PurchaseProductTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public PurchaseProductTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }


    [Fact]
    public async Task Get_PurchaseProductList()
    {
        // ARRANGE
        PurchaseProduct testPurchaseProduct = new()
        {
            //PurchaseProductname = "TestName",
            Id = 77
        };

        await client.PostAsJsonAsync("api/purchaseProduct/add", testPurchaseProduct);

        // ACT
        List<PurchaseProduct>? result = await client.GetFromJsonAsync<List<PurchaseProduct>>("api/purchaseProduct/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_PurchaseProduct_ById()
    {
        // ARRANGE
        PurchaseProduct testPurchaseProduct = new()
        {
            //PurchaseProductname = "TestName",
            Id = 78
        };

        await client.PostAsJsonAsync("api/purchaseProduct/add", testPurchaseProduct);

        // ACT
        PurchaseProduct? result = await client.GetFromJsonAsync<PurchaseProduct>($"api/purchaseProduct/get/{testPurchaseProduct.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testPurchaseProduct.Id, result.Id);
    }

    [Fact]
    public async Task Get_PurchaseProduct_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<PurchaseProduct>($"api/purchaseProduct/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_PurchaseProduct_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<PurchaseProduct>($"api/purchaseProduct/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_PurchaseProduct()
    {
        // ARRANGE
        PurchaseProduct testPurchaseProduct = new()
        {
            //PurchaseProductname = "TestName",
            Id = 80
        };

        // ACT
        await client.PostAsJsonAsync("api/purchaseProduct/add", testPurchaseProduct);
        PurchaseProduct? result = await client.GetFromJsonAsync<PurchaseProduct>($"api/purchaseProduct/get/{testPurchaseProduct.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testPurchaseProduct.Id, result.Id);
    }

    [Fact]
    public async Task Create_PurchaseProduct_When_AlreadyExists()
    {
        // ARRANGE
        PurchaseProduct existing = new()
        {
            //PurchaseProductname = "TestName",
            Id = 101
        };

        // ACT
        await client.PostAsJsonAsync("api/purchaseProduct/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/purchaseProduct/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_PurchaseProduct()
    {
        // ARRANGE
        PurchaseProduct testPurchaseProduct = new()
        {
            // PurchaseProductname = "TestName",
            Id = 81
        };

        await client.PostAsJsonAsync("api/purchaseProduct/add", testPurchaseProduct);

        // ACT
        //testPurchaseProduct.PurchaseProductname = "EditedTestName";
        await client.PatchAsJsonAsync("api/purchaseProduct/update", testPurchaseProduct);

        PurchaseProduct? result = await client.GetFromJsonAsync<PurchaseProduct>($"api/purchaseProduct/get/{testPurchaseProduct.Id}");


        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testPurchaseProduct.Id, result.Id);
    }

    [Fact]
    public async Task Delete_PurchaseProduct()
    {
        // ARRANGE
        PurchaseProduct testPurchaseProduct = new()
        {
            //PurchaseProductname = "TestName",
            Id = 82
        };

        await client.PostAsJsonAsync("api/purchaseProduct/add", testPurchaseProduct);

        // ACT
        await client.DeleteAsync($"api/purchaseProduct/delete/{testPurchaseProduct.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<PurchaseProduct>($"api/purchaseProduct/get/{testPurchaseProduct.Id}");
        });
    }

    [Fact]
    public async Task Delete_PurchaseProduct_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/purchaseProduct/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}
