using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class AddonFlavorTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public AddonFlavorTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public async Task Get_AddonFlavorList()
    {
        // ARRANGE
        Addonflavor testAddonFlavor = new()
        {
            Id = 77,
            Flavor = "Some Test Name"
        };

        await client.PostAsJsonAsync("api/AddonFlavor/add", testAddonFlavor);

        // ACT
        List<Addonflavor>? result = await client.GetFromJsonAsync<List<Addonflavor>>("api/AddonFlavor/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_AddonFlavor_ById()
    {
        // ARRANGE
        Addonflavor testAddonFlavor = new()
        {
            Id = 78,
            Flavor = "Some Test Name"
        };

        await client.PostAsJsonAsync("api/AddonFlavor/add", testAddonFlavor);

        // ACT
        Addonflavor? result = await client.GetFromJsonAsync<Addonflavor>($"api/AddonFlavor/get/{testAddonFlavor.Id}");

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testAddonFlavor.Flavor, result.Flavor);
        Assert.Equal(testAddonFlavor.Id, result.Id);
    }

    [Fact]
    public async Task Get_AddonFlavor_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addonflavor>($"api/AddonFlavor/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_AddonFlavor_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addonflavor>($"api/AddonFlavor/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_AddonFlavor()
    {
        // ARRANGE
        Addonflavor testAddonFlavor = new()
        {
            Id = 80,
            Flavor = "Some Test Name"
        };

        // ACT
        await client.PostAsJsonAsync("api/AddonFlavor/add", testAddonFlavor);
        Addonflavor? result = await client.GetFromJsonAsync<Addonflavor>($"api/AddonFlavor/get/{testAddonFlavor.Id}");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testAddonFlavor.Flavor, result.Flavor);
        Assert.Equal(testAddonFlavor.Id, result.Id);
    }

    [Fact]
    public async Task Create_AddonFlavor_When_AlreadyExists()
    {
        // ARRANGE
        Addonflavor existing = new()
        {
            Id = 101,
            Flavor = "Some Test Name"
        };

        // ACT
        await client.PostAsJsonAsync("api/AddonFlavor/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/AddonFlavor/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_AddonFlavor()
    {
        // ARRANGE
        Addonflavor testAddonFlavor = new()
        {
            Id = 81,
            Flavor = "Some Test Name"
        };

        await client.PostAsJsonAsync("api/AddonFlavor/add", testAddonFlavor);

        // ACT
        testAddonFlavor.Flavor = "Flavor Update";
        await client.PatchAsJsonAsync("api/AddonFlavor/update", testAddonFlavor);

        Addonflavor? result = await client.GetFromJsonAsync<Addonflavor>($"api/AddonFlavor/get/{testAddonFlavor.Id}");


        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testAddonFlavor.Flavor, result.Flavor);
        Assert.Equal(testAddonFlavor.Id, result.Id);
    }

    [Fact]
    public async Task Delete_AddonFlavor()
    {
        // ARRANGE
        Addonflavor testAddonFlavor = new()
        {
            Id = 82,
            Flavor = "Some Test Name"
        };

        await client.PostAsJsonAsync("api/AddonFlavor/add", testAddonFlavor);

        // ACT
        await client.DeleteAsync($"api/AddonFlavor/delete/{testAddonFlavor.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addonflavor>($"api/AddonFlavor/get/{testAddonFlavor.Id}");
        });
    }

    [Fact]
    public async Task Delete_AddonFlavor_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/AddonFlavor/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}