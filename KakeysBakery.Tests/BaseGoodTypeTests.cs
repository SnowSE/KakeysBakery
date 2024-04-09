using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class BaseGoodTypeTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public BaseGoodTypeTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }
    [Fact]
    public async Task Get_BaseGoodTypeList()
    {
        // ARRANGE
        Basegoodtype testBasegoodtype = new()
        {
            Id = 77
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", testBasegoodtype);

        // ACT
        List<Basegoodtype>? result = await client.GetFromJsonAsync<List<Basegoodtype>>("api/Basegoodtype/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_BaseGoodType_ById()
    {
        // ARRANGE
        Basegoodtype testBasegoodtype = new()
        {
            Id = 78
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", testBasegoodtype);

        // ACT
        Basegoodtype? result = await client.GetFromJsonAsync<Basegoodtype>($"api/Basegoodtype/get/{testBasegoodtype.Id}");

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testBasegoodtype.Id, result.Id);
    }

    [Fact]
    public async Task Get_BaseGoodType_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegoodtype>($"api/Basegoodtype/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_BaseGoodType_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegoodtype>($"api/Basegoodtype/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_BaseGoodType()
    {
        // ARRANGE
        Basegoodtype testBasegoodtype = new()
        {
            Id = 80
        };

        // ACT
        await client.PostAsJsonAsync("api/Basegoodtype/add", testBasegoodtype);
        Basegoodtype? result = await client.GetFromJsonAsync<Basegoodtype>($"api/Basegoodtype/get/{testBasegoodtype.Id}");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testBasegoodtype.Id, result.Id);
    }

    [Fact]
    public async Task Create_BaseGoodType_When_AlreadyExists()
    {
        // ARRANGE
        Basegoodtype existing = new()
        {
            Id = 101
        };

        // ACT
        await client.PostAsJsonAsync("api/Basegoodtype/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/Basegoodtype/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_BaseGoodType()
    {
        // ARRANGE
        Basegoodtype testBasegoodtype = new()
        {
            Id = 81
        };
        testBasegoodtype.Basegood = "first test def";
        await client.PostAsJsonAsync("api/Basegoodtype/add", testBasegoodtype);

        // ACT
        testBasegoodtype.Basegood = "second test definition";

        //testBasegoodtype.Basegoodtypename = "EditedTestName";
        await client.PatchAsJsonAsync("api/Basegoodtype/update", testBasegoodtype);

        Basegoodtype? result = await client.GetFromJsonAsync<Basegoodtype>($"api/Basegoodtype/get/{testBasegoodtype.Id}");


        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testBasegoodtype.Id, result.Id);
    }

    [Fact]
    public async Task Delete_BaseGood()
    {
        // ARRANGE
        Basegoodtype testBasegoodtype = new()
        {
            Id = 82
        };

        await client.PostAsJsonAsync("api/Basegoodtype/add", testBasegoodtype);

        // ACT
        await client.DeleteAsync($"api/Basegoodtype/delete/{testBasegoodtype.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Basegoodtype>($"api/Basegoodtype/get/{testBasegoodtype.Id}");
        });
    }

    [Fact]
    public async Task Delete_BaseGood_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/Basegoodtype/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}