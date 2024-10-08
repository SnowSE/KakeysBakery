using System.Net.Http.Json;

namespace KakeysBakeryTests.CRUD_Tests;

public class BaseGoodTests : IClassFixture<BakeryFactory>
{

    /// <summary>
    /// ///////////////////////////////////////////////////////////////////test GetBasegoodsFromTypeAsync////////////////////////////////////////
    /// </summary>
    public HttpClient client { get; set; }
    public BaseGoodTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }
    [Fact]
    public async Task Get_BaseGoodList()
    {
        // ARRANGE
        Basegood testBaseGood = new()
        {
            //Basegoodname = "TestName",
            Id = 77,
            Suggestedprice = (decimal)100.25,
            //Flavor = "testFlavor"
        };

        await client.PostAsJsonAsync("api/basegood/add", testBaseGood);

        // ACT
        List<Basegood>? result = await client.GetFromJsonAsync<List<Basegood>>("api/basegood/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_BaseGood_ById()
    {
        // ARRANGE
        Basegood testBaseGood = new()
        {
            //Basegoodname = "TestName",
            Id = 1000,
            Suggestedprice = (decimal)100.25,
            //Flavor = "testFlavor"
        };

        await client.PostAsJsonAsync("api/basegood/add", testBaseGood);

        // ACT
        Basegood? result = await client.GetFromJsonAsync<Basegood>($"api/basegood/get/{testBaseGood.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testBaseGood.Suggestedprice, result.Suggestedprice);
        Assert.Equal(testBaseGood.Flavor, result.Flavor);
        Assert.Equal(testBaseGood.Id, result.Id);
    }

    [Fact]
    public async Task Get_BaseGood_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegood>($"api/basegood/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_BaseGood_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegood>($"api/basegood/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_BaseGood()
    {
        // ARRANGE
        Basegood testBaseGood = new()
        {
            //Basegoodname = "TestName",
            Id = 1000,
            Suggestedprice = (decimal)100.25,
            //Flavor = "testFlavor"
        };

        // ACT
        await client.PostAsJsonAsync("api/basegood/add", testBaseGood);
        Basegood? result = await client.GetFromJsonAsync<Basegood>($"api/basegood/get/{testBaseGood.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testBaseGood.Suggestedprice, result.Suggestedprice);
        Assert.Equal(testBaseGood.Flavor, result.Flavor);
        Assert.Equal(testBaseGood.Id, result.Id);
    }

    [Fact]
    public async Task Create_BaseGood_When_AlreadyExists()
    {
        // ARRANGE
        Basegood existing = new()
        {
            //Basegoodname = "TestName",
            Id = 101,
            Suggestedprice = (decimal)100.25,
            // Flavor = "testFlavor"
        };

        // ACT
        await client.PostAsJsonAsync("api/basegood/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/basegood/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_BaseGood()
    {
        // ARRANGE
        Basegood testBaseGood = new()
        {
            // Basegoodname = "TestName",
            Id = 81,
            Suggestedprice = (decimal)100.25,
            // Flavor = "testFlavor"
        };

        await client.PostAsJsonAsync("api/basegood/add", testBaseGood);

        // ACT
        testBaseGood.Suggestedprice = (decimal)111.25;
        //testBaseGood.Basegoodname = "EditedTestName";
        await client.PatchAsJsonAsync("api/basegood/update", testBaseGood);

        Basegood? result = await client.GetFromJsonAsync<Basegood>($"api/basegood/get/{testBaseGood.Id}");


        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testBaseGood.Suggestedprice, result.Suggestedprice);
        Assert.Equal(testBaseGood.Flavor, result.Flavor);
        Assert.Equal(testBaseGood.Id, result.Id);
    }

    [Fact]
    public async Task Delete_BaseGood()
    {
        // ARRANGE
        Basegood testBaseGood = new()
        {
            //Basegoodname = "TestName",
            Id = 82,
            Suggestedprice = (decimal)100.25,
            //Flavor = "testFlavor"
        };

        await client.PostAsJsonAsync("api/basegood/add", testBaseGood);

        // ACT
        await client.DeleteAsync($"api/basegood/delete/{testBaseGood.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegood>($"api/basegood/get/{testBaseGood.Id}");
        });
    }

    [Fact]
    public async Task Delete_BaseGood_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/basegood/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Get_BaseGood_From_BaseGoodType()
    {
        // ARRANGE
        Basegoodflavor goodFlavor = new()
        {
            Id = 90
        };

        Basegoodtype goodType = new()
        {
            Id = 90
        };

        Basegood good = new()
        {
            Id = 90,
            Typeid = 90,
            Flavorid = 90
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", goodType);
        await client.PostAsJsonAsync("api/basegoodflavor/add", goodFlavor);
        await client.PostAsJsonAsync("api/basegood/add", good);

        // ACT & Assert
        List<Basegood>? resultList = await client.GetFromJsonAsync<List<Basegood>>($"api/basegood/get_from_type/{goodType.Id}");
        Assert.NotNull(resultList);
        Assert.NotEmpty(resultList);

        Basegood result = resultList.First();
        Assert.Equal(good.Id, result.Id);
    }

    [Fact]
    public async Task Get_BaseGood_From_BaseGoodFlavor()
    {
        // ARRANGE
        Basegoodflavor goodFlavor = new()
        {
            Id = 91
        };

        Basegoodtype goodType = new()
        {
            Id = 91
        };

        Basegood good = new()
        {
            Id = 91,
            Typeid = 91,
            Flavorid = 91
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", goodType);
        await client.PostAsJsonAsync("api/basegoodflavor/add", goodFlavor);
        await client.PostAsJsonAsync("api/basegood/add", good);

        // ACT & Assert
        Basegood? resultList = await client.GetFromJsonAsync<Basegood>($"api/basegood/get_from_flavor/{goodType.Id}/{goodFlavor.Id}");
        Assert.NotNull(resultList);

        Basegood result = resultList;
        Assert.Equal(good.Id, result.Id);
    }
}