using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class BaseGoodTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public BaseGoodTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public void CanPassATest()
    {
        Assert.Equal(1, 1); 
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
            Id = 78,
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
            Id = 80,
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
}