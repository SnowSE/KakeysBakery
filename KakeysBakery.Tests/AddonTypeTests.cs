using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class AddonTypeTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public AddonTypeTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public async Task Get_AddonTypeList()
    {
        // ARRANGE
        Addontype testAddonType = new()
        {
            Id = 77,
            Basetype = "test type"
        };

        await client.PostAsJsonAsync("api/AddonType/add", testAddonType);

        // ACT
        List<Addontype>? result = await client.GetFromJsonAsync<List<Addontype>>("api/AddonType/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_AddonType_ById()
    {
        // ARRANGE
        Addontype testAddonType = new()
        {
            Id = 78,
            Basetype = "test type"
        };

        await client.PostAsJsonAsync("api/AddonType/add", testAddonType);

        // ACT
        Addontype? result = await client.GetFromJsonAsync<Addontype>($"api/AddonType/get/{testAddonType.Id}");

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testAddonType.Id, result.Id);
    }

    [Fact]
    public async Task Get_AddonType_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addontype>($"api/AddonType/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_AddonType_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addontype>($"api/AddonType/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_AddonType()
    {
        // ARRANGE
        Addontype testAddonType = new()
        {
            Id = 80,
            Basetype = "test type"
        };

        // ACT
        await client.PostAsJsonAsync("api/AddonType/add", testAddonType);
        Addontype? result = await client.GetFromJsonAsync<Addontype>($"api/AddonType/get/{testAddonType.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testAddonType.Id, result.Id);
    }

    [Fact]
    public async Task Create_AddonType_When_AlreadyExists()
    {
        // ARRANGE
        Addontype existing = new()
        {
            Id = 101,
            Basetype = "test type"
        };

        // ACT
        await client.PostAsJsonAsync("api/AddonType/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/AddonType/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_AddonType()
    {
        // ARRANGE
        Addontype testAddonType = new()
        {
            Id = 81,
            Basetype = "test type"
        };

        await client.PostAsJsonAsync("api/AddonType/add", testAddonType);

        // ACT
        testAddonType.Basetype = "edited test type";
        //testAddonType.Addontypename = "EditedTestName";
        await client.PatchAsJsonAsync("api/AddonType/update", testAddonType);

        Addontype? result = await client.GetFromJsonAsync<Addontype>($"api/AddonType/get/{testAddonType.Id}");


        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testAddonType.Id, result.Id);
    }

    [Fact]
    public async Task Delete_AddonType()
    {
        // ARRANGE
        Addontype testAddonType = new()
        {
            Id = 82,
            Basetype = "test type"
        };

        await client.PostAsJsonAsync("api/AddonType/add", testAddonType);

        // ACT
        await client.DeleteAsync($"api/AddonType/delete/{testAddonType.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addontype>($"api/AddonType/get/{testAddonType.Id}");
        });
    }

    [Fact]
    public async Task Delete_AddonType_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/AddonType/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}
