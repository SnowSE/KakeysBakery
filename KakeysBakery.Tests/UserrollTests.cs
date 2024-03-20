using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class UserroleTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public UserroleTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public async Task Get_UserroleList()
    {
        // ARRANGE
        Userrole testUserrole = new()
        {
            Id = 77,
            Userrole1 = "testrole"
        };

        await client.PostAsJsonAsync("api/Userrole/add", testUserrole);

        // ACT
        List<Userrole>? result = await client.GetFromJsonAsync<List<Userrole>>("api/Userrole/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_Userrole_ById()
    {
        // ARRANGE
        Userrole testUserrole = new()
        {
            Id = 78,
            Userrole1 = "testrole"
        };

        await client.PostAsJsonAsync("api/Userrole/add", testUserrole);

        // ACT
        Userrole? result = await client.GetFromJsonAsync<Userrole>($"api/Userrole/get/{testUserrole.Id}");

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testUserrole.Userrole1, result.Userrole1);
        Assert.Equal(testUserrole.Id, result.Id);
    }

    [Fact]
    public async Task Get_Userrole_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Userrole>($"api/Userrole/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_Userrole_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Userrole>($"api/Userrole/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_Userrole()
    {
        // ARRANGE
        Userrole testUserrole = new()
        {
            Id = 80,
            Userrole1 = "testrole"
        };

        // ACT
        await client.PostAsJsonAsync("api/Userrole/add", testUserrole);
        Userrole? result = await client.GetFromJsonAsync<Userrole>($"api/Userrole/get/{testUserrole.Id}");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testUserrole.Userrole1, result.Userrole1);
        Assert.Equal(testUserrole.Id, result.Id);
    }

    [Fact]
    public async Task Create_Userrole_When_AlreadyExists()
    {
        // ARRANGE
        Userrole existing = new()
        {
            Id = 101,
            Userrole1 = "testrole"
        };

        // ACT
        await client.PostAsJsonAsync("api/Userrole/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/Userrole/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_Userrole()
    {
        // ARRANGE
        Userrole testUserrole = new()
        {
            Id = 81,
            Userrole1 = "testrole"
        };

        await client.PostAsJsonAsync("api/Userrole/add", testUserrole);

        // ACT
        testUserrole.Userrole1 = "testrole Update";
        //testUserrole.Userrolename = "EditedTestName";
        await client.PatchAsJsonAsync("api/Userrole/update", testUserrole);

        Userrole? result = await client.GetFromJsonAsync<Userrole>($"api/Userrole/get/{testUserrole.Id}");


        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testUserrole.Userrole1, result.Userrole1);
        Assert.Equal(testUserrole.Id, result.Id);
    }

    [Fact]
    public async Task Delete_Userrole()
    {
        // ARRANGE
        Userrole testUserrole = new()
        {
            Id = 82,
            Userrole1 = "testrole"
        };

        await client.PostAsJsonAsync("api/Userrole/add", testUserrole);

        // ACT
        await client.DeleteAsync($"api/Userrole/delete/{testUserrole.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Userrole>($"api/Userrole/get/{testUserrole.Id}");
        });
    }

    [Fact]
    public async Task Delete_Userrole_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/Userrole/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}