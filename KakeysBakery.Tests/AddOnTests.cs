using System.Net.Http.Json;

namespace KakeysBakeryTests;

public class AddOnTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public AddOnTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public void CanPassATest()
    {
        Assert.Equal(1, 1); 
    }

    //We were failing to relace the production database with a testing database. 
    //This is because the Posgres Contect was getting the connections string from the enviroment variable and not useing the one set in program.cs
    //Be sure to check for that when we rescaffold
    [Fact]
    public async Task Get_AddonList()
    {
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            //Addontypename = "TestName",
            Id = 77,
            Suggestedprice = (decimal)100.25
        };

        await client.PostAsJsonAsync("api/addon/add", testaddon);

        // ACT
        List<Addon>? addons = await client.GetFromJsonAsync<List<Addon>>("api/addon/getall");

        // ASSERT
        Assert.NotNull(addons);
        Assert.NotEmpty(addons);
    }

    [Fact]
    public async Task Get_Addon_ById()
    {
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            //Addontypename = "TestName",
            Id = 78,
            Suggestedprice = (decimal)100.25
        };

        await client.PostAsJsonAsync("api/addon/add", testaddon);

        // ACT
        Addon? result = await client.GetFromJsonAsync<Addon>($"api/addon/get/{testaddon.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testaddon.Suggestedprice, result.Suggestedprice);
        //.Equal(testaddon.Flavor, result.Flavor);
        Assert.Equal(testaddon.Id, result.Id);
        Assert.Equal(testaddon.Description, result.Description);
    }

    [Fact]
    public async Task Get_AddOn_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addon>($"api/addon/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_Addon_ByName()
    {
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            //Addontypename = "UniqueTestName",
            Id = 82,
            Suggestedprice = (decimal)100.25
        };

        await client.PostAsJsonAsync("api/addon/add", testaddon);

        // ACT
        //Addon? result = await client.GetFromJsonAsync<Addon>($"api/addon/get_by_name/{testaddon.Addontypename}");

        // ASSERT
        //Assert.NotNull(result);

        //Assert.Equal(testaddon.Suggestedprice, result.Suggestedprice);
        ////Assert.Equal(testaddon.Flavor, result.Flavor);
        //Assert.Equal(testaddon.Id, result.Id);
        //Assert.Equal(testaddon.Description, result.Description);
    }

    [Fact]
    public async Task Get_AddOn_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addon>($"api/addon/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_AddOn()
    {
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            //Addontypename = "TestName",
            Id = 79,
            Suggestedprice = (decimal)100.25
        };

        await client.PostAsJsonAsync("api/addon/add", testaddon);

        // ACT
        await client.PostAsJsonAsync("api/addon/add", testaddon);
        Addon? result = await client.GetFromJsonAsync<Addon>($"api/addon/get/{testaddon.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testaddon.Suggestedprice, result.Suggestedprice);
        //Assert.Equal(testaddon.Flavor, result.Flavor);
        Assert.Equal(testaddon.Id, result.Id);
        Assert.Equal(testaddon.Description, result.Description);
    }

    [Fact]
    public async Task Create_AddOn_When_AlreadyExists()
    {
        // ARRANGE
        Addon existing = new Addon()
        {
            Description = "TestDesc",
            //Addontypename = "TestName",
            Id = 101,
            Suggestedprice = (decimal)100.25
        };

        // ACT
        await client.PostAsJsonAsync("api/addon/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/addon/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_AddOn()
    {
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            //Addontypename = "TestName",
            Id = 80,
            Suggestedprice = (decimal)100.25
        };

        await client.PostAsJsonAsync("api/addon/add", testaddon);

        // ACT
        testaddon.Description = "EditedTestDescription";
        await client.PatchAsJsonAsync("api/addon/update", testaddon);
        Addon? result = await client.GetFromJsonAsync<Addon>($"api/addon/get/{testaddon.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testaddon.Description, result.Description);
        Assert.Equal(testaddon.Id, result.Id);
    }

    [Fact]
    public async Task Delete_AddOn()
    { 
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            //Addontypename = "TestName",
            Id = 81,
            Suggestedprice = (decimal)100.25
        };

        await client.PostAsJsonAsync("api/addon/add", testaddon);

        // ACT
        await client.DeleteAsync($"api/addon/delete/{testaddon.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addon>($"api/addon/get/{testaddon.Id}");
        });
    }

    [Fact]
    public async Task Delete_AddOn_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/addon/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}