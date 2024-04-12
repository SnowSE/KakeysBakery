using System.Net.Http.Json;

namespace KakeysBakeryTests.CRUD_Tests;

public class ProductAddonBasegoodTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public ProductAddonBasegoodTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public async Task Get_ProductAddonBasegoodList()
    {
        // ARRANGE
        ProductAddonBasegood testProductAddonBasegood = new()
        {
            Id = 77
        };

        await client.PostAsJsonAsync("api/productAddonBasegood/add", testProductAddonBasegood);

        // ACT
        List<ProductAddonBasegood>? result = await client.GetFromJsonAsync<List<ProductAddonBasegood>>("api/productAddonBasegood/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_ProductAddonBasegood_ById()
    {
        // ARRANGE
        ProductAddonBasegood testProductAddonBasegood = new()
        {
            Id = 78
        };

        await client.PostAsJsonAsync("api/productAddonBasegood/add", testProductAddonBasegood);

        // ACT
        ProductAddonBasegood? result = await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get/{testProductAddonBasegood.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testProductAddonBasegood.Id, result.Id);
    }

    [Fact]
    public async Task Get_ProductAddonBasegood_BySelectionId_And_TypeId()
    {
        // ARRANGE
        Basegoodtype type = new()
        {
            Id = 1101
        };

        Basegood good = new()
        {
            Id = 1100,
            Typeid = type.Id
        };

        ProductAddonBasegood pab = new()
        {
            Id = 1102,
            Basegoodid = good.Id,
        };

        await client.PostAsJsonAsync("api/basegoodtype/add", type);
        await client.PostAsJsonAsync("api/basegood/add", good);
        await client.PostAsJsonAsync("api/productAddonBasegood/add", pab);

        // ACT
        ProductAddonBasegood? result = await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get/{type.Id}/{good.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(pab.Id, result.Id);
    }

    [Fact]
    public async Task Get_ProductAddonBasegood_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_ProductAddonBasegood_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_ProductAddonBasegood()
    {
        // ARRANGE
        ProductAddonBasegood testProductAddonBasegood = new()
        {
            Id = 80
        };

        // ACT
        await client.PostAsJsonAsync("api/productAddonBasegood/add", testProductAddonBasegood);
        ProductAddonBasegood? result = await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get/{testProductAddonBasegood.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testProductAddonBasegood.Id, result.Id);
    }

    [Fact]
    public async Task Create_ProductAddonBasegood_When_AlreadyExists()
    {
        // ARRANGE
        ProductAddonBasegood existing = new()
        {
            Id = 101
        };

        // ACT
        await client.PostAsJsonAsync("api/productAddonBasegood/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/productAddonBasegood/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_ProductAddonBasegood()
    {
        // ARRANGE
        ProductAddonBasegood testProductAddonBasegood = new()
        {
            Id = 81
        };

        await client.PostAsJsonAsync("api/productAddonBasegood/add", testProductAddonBasegood);

        // ACT
        //testProductAddonBasegood.Basegoodname = "EditedTestName";
        await client.PatchAsJsonAsync("api/productAddonBasegood/update", testProductAddonBasegood);

        ProductAddonBasegood? result = await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get/{testProductAddonBasegood.Id}");


        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testProductAddonBasegood.Id, result.Id);
    }

    [Fact]
    public async Task Delete_ProductAddonBasegood()
    {
        // ARRANGE
        ProductAddonBasegood testProductAddonBasegood = new()
        {
            Id = 82
        };

        await client.PostAsJsonAsync("api/productAddonBasegood/add", testProductAddonBasegood);

        // ACT
        await client.DeleteAsync($"api/productAddonBasegood/delete/{testProductAddonBasegood.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<ProductAddonBasegood>($"api/productAddonBasegood/get/{testProductAddonBasegood.Id}");
        });
    }

    [Fact]
    public async Task Delete_ProductAddonBasegood_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/productAddonBasegood/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}