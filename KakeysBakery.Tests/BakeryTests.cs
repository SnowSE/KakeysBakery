

using System.Net.Http.Json;

namespace KakeysBakery.Tests;

public class BakeryTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public BakeryFactory bakeryFactory { get; set; }
    public BakeryTests(BakeryFactory Factory)
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
    public async Task CanMakeAddOnInDB()
    { 
        //arrange
        Addon testaddon = new Addon();
        testaddon.Description = "TestDesc";
        testaddon.Addontypename = "TestName";
        testaddon.Id = 77;
        testaddon.Suggestedprice = (decimal)100.25;
        List<Addon> addons = new();

        //act
        await client.PostAsJsonAsync("api/addon/add", testaddon);
        addons = await client.GetFromJsonAsync<List<Addon>>("api/addon/getall");
        Addon result = addons.FirstOrDefault(a => a.Id == 77);
        //assert
        Assert.Equal(testaddon.Suggestedprice, result.Suggestedprice);
        Assert.Equal(testaddon.Flavor, result.Flavor);
        Assert.Equal(testaddon.Id, result.Id);
        Assert.Equal(testaddon.Description, result.Description);
    }
}