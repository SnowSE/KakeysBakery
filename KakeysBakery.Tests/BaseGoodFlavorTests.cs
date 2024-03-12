using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class BaseGoodFlavorTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public BaseGoodFlavorTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public void CanPassATest()
    {
        Assert.Equal(1, 1);
    }

    [Fact]
    public async Task Get_BaseGoodFlavorList()
    {
        // ARRANGE
        Basegoodflavor testBaseGoodFlavor = new()
        {
            //Basegoodflavorname = "TestName",
            Id = 77,
            Flavorname = "Test"
        };

        await client.PostAsJsonAsync("api/BaseGoodFlavor/add", testBaseGoodFlavor);

        // ACT
        List<Basegoodflavor>? result = await client.GetFromJsonAsync<List<Basegoodflavor>>("api/BaseGoodFlavor/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_BaseGoodFlavor_ById()
    {
        // ARRANGE
        Basegoodflavor testBaseGoodFlavor = new()
        {
            //Basegoodflavorname = "TestName",
            Id = 78,
            Flavorname = "Test"
        };

        await client.PostAsJsonAsync("api/BaseGoodFlavor/add", testBaseGoodFlavor);

        // ACT
        Basegoodflavor? result = await client.GetFromJsonAsync<Basegoodflavor>($"api/BaseGoodFlavor/get/{testBaseGoodFlavor.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testBaseGoodFlavor.Id, result.Id);
    }

    [Fact]
    public async Task Get_BaseGoodFlavor_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegoodflavor>($"api/BaseGoodFlavor/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_BaseGoodFlavor_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegoodflavor>($"api/BaseGoodFlavor/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_BaseGoodFlavor()
    {
        // ARRANGE
        Basegoodflavor testBaseGoodFlavor = new()
        {
            //Basegoodflavorname = "TestName",
            Id = 80,
            Flavorname = "Test"
        };

        // ACT
        await client.PostAsJsonAsync("api/BaseGoodFlavor/add", testBaseGoodFlavor);
        Basegoodflavor? result = await client.GetFromJsonAsync<Basegoodflavor>($"api/BaseGoodFlavor/get/{testBaseGoodFlavor.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testBaseGoodFlavor.Id, result.Id);
    }

    [Fact]
    public async Task Create_BaseGoodFlavor_When_AlreadyExists()
    {
        // ARRANGE
        Basegoodflavor existing = new()
        {
            //Basegoodflavorname = "TestName",
            Id = 101,
            Flavorname = "Test"
        };

        // ACT
        await client.PostAsJsonAsync("api/BaseGoodFlavor/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/BaseGoodFlavor/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_BaseGoodFlavor()
    {
        // ARRANGE
        Basegoodflavor testBaseGoodFlavor = new()
        {
            // Basegoodflavorname = "TestName",
            Id = 81,
            Flavorname = "Test"
        };

        await client.PostAsJsonAsync("api/BaseGoodFlavor/add", testBaseGoodFlavor);

        // ACT
        testBaseGoodFlavor.Flavorname = "Altered Test Name";
        //testBaseGoodFlavor.Basegoodflavorname = "EditedTestName";
        await client.PatchAsJsonAsync("api/BaseGoodFlavor/update", testBaseGoodFlavor);

        Basegoodflavor? result = await client.GetFromJsonAsync<Basegoodflavor>($"api/BaseGoodFlavor/get/{testBaseGoodFlavor.Id}");


        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testBaseGoodFlavor.Id, result.Id);
    }

    [Fact]
    public async Task Delete_BaseGoodFlavor()
    {
        // ARRANGE
        Basegoodflavor testBaseGoodFlavor = new()
        {
            //Basegoodflavorname = "TestName",
            Id = 82,
            Flavorname = "Test"
        };

        await client.PostAsJsonAsync("api/BaseGoodFlavor/add", testBaseGoodFlavor);

        // ACT
        await client.DeleteAsync($"api/BaseGoodFlavor/delete/{testBaseGoodFlavor.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegoodflavor>($"api/BaseGoodFlavor/get/{testBaseGoodFlavor.Id}");
        });
    }

    [Fact]
    public async Task Delete_BaseGoodFlavor_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/BaseGoodFlavor/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}