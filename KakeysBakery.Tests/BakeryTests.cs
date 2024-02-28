

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

    [Fact]
    public async Task Can_Create_AddOn()
    {
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            Addontypename = "TestName",
            Id = 77,
            Suggestedprice = (decimal)100.25
        };

        // ACT
        await client.PostAsJsonAsync("api/addon/add", testaddon);
        List<Addon> addons = await client.GetFromJsonAsync<List<Addon>>("api/addon/getall");
        Addon result = addons.FirstOrDefault(a => a.Id == 77);

        // ASSERT
        Assert.NotNull(addons);
        Assert.NotNull(result);

        Assert.Equal(testaddon.Suggestedprice, result.Suggestedprice);
        Assert.Equal(testaddon.Flavor, result.Flavor);
        Assert.Equal(testaddon.Id, result.Id);
        Assert.Equal(testaddon.Description, result.Description);
    }

    [Fact]
    public async Task Can_Edit_AddOn()
    {
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            Addontypename = "TestName",
            Id = 77,
            Suggestedprice = (decimal)100.25
        };

        // ACT
        testaddon.Description = "EditedTestDescription";
        await client.PatchAsJsonAsync("api/addon/update", testaddon);
        List<Addon>  addons = await client.GetFromJsonAsync<List<Addon>>("api/addon/getall");
        Addon result = addons.FirstOrDefault(a => a.Id == 77);

        // ASSERT
        Assert.NotNull(addons);
        Assert.NotNull(result);

        Assert.Equal(testaddon.Description, result.Description);
        Assert.Equal(testaddon.Id, result.Id);
    }

    //We were failing to relace the production database with a testing database. 
    //This is because the Posgres Contect was getting the connections string from the enviroment variable and not useing the one set in program.cs
    //Be sure to check for that when we rescaffold
    [Fact]
    public async Task Can_Delete_AddOn()
    { 
        // ARRANGE
        Addon testaddon = new Addon()
        {
            Description = "TestDesc",
            Addontypename = "TestName",
            Id = 77,
            Suggestedprice = (decimal)100.25
        };
    
        // ACT
        await client.DeleteAsync($"api/addon/delete/{testaddon.Id}");
        List<Addon> addons = await client.GetFromJsonAsync<List<Addon>>("api/addon/getall");
        Addon result = addons.FirstOrDefault(a => a.Id == 77);

        // ASSERT

        Assert.Null(result);
    }

    [Fact]
    public async Task CRUD_For_BaseGoods()
    {
        //arrange
        Basegood testBaseGood = new();
        testBaseGood.Basegoodname = "TestName";
        testBaseGood.Id = 77;
        testBaseGood.Suggestedprice = (decimal)100.25;
        testBaseGood.Flavor = "testFlavor";
        List<Basegood> basegoods = new();

        //act
        await client.PostAsJsonAsync("api/basegood/add", testBaseGood);
        basegoods = await client.GetFromJsonAsync<List<Basegood>>("api/basegood/getall");
        Basegood result = basegoods.FirstOrDefault(a => a.Id == 77);
        //assert
        Assert.Equal(testBaseGood.Suggestedprice, result.Suggestedprice);
        Assert.Equal(testBaseGood.Flavor, result.Flavor);
        Assert.Equal(testBaseGood.Id, result.Id);

        //act
        testBaseGood.Basegoodname = "EditedTestName";
        await client.PatchAsJsonAsync("api/basegood/update", testBaseGood);
        basegoods = await client.GetFromJsonAsync<List<Basegood>>("api/basegood/getall");
        result = basegoods.FirstOrDefault(a => a.Id == 77);
        //assert
        Assert.Equal(testBaseGood.Basegoodname, result.Basegoodname);
        Assert.Equal(testBaseGood.Id, result.Id);
        //act
        await client.DeleteAsync($"api/basegood/delete/{testBaseGood.Id}");
        basegoods = await client.GetFromJsonAsync<List<Basegood>>("api/basegood/getall");
        result = basegoods.FirstOrDefault(a => a.Id == 77);
        //assert
        Assert.Null(result);
    }
}

public class PurchaseTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public BakeryFactory bakeryFactory { get; set; }
    public PurchaseTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public async Task canAddPurchases()
    {
        //arrange
        Purchase testPurchase = new Purchase();
        testPurchase.Id = 245;
        testPurchase.Actualprice = (decimal)100.40;

        //act
        await client.PostAsJsonAsync("api/Purchase/add", testPurchase);
        List<Purchase> gotten = await client.GetFromJsonAsync<List<Purchase>>("api/Purchase/getall");
        Purchase result = gotten.FirstOrDefault(p => p.Id == 245);

        //assert
        Assert.Equal(testPurchase.Id, result.Id);
    }
}
