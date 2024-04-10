using System.Net.Http.Json;

namespace KakeysBakeryTests.Logic_Tests;

public class CartLogicTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public CartLogicTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();

    }
    private async Task setUpTest(int customerId, int BasegoodId, int allOtherId)
    {
        Customer testCustomer = new Customer()
        {
            Id = customerId
        };
        await client.PostAsJsonAsync("api/Cutomer/add", testCustomer);
        Basegoodflavor testflavor = new Basegoodflavor() { Id = allOtherId, Flavorname = "test Flavor Name" };
        BasegoodSize testSize = new BasegoodSize() { Id = allOtherId, Size = "test pan size" };
        Basegoodtype testType = new Basegoodtype() { Id = allOtherId, Basegood = "a test cake" };
        Basegood testGood = new Basegood() { Id = BasegoodId, Flavorid = allOtherId, Sizeid = allOtherId, Typeid = allOtherId };
        await client.PostAsJsonAsync("api/Basegoodflavor/add", testflavor);
        await client.PostAsJsonAsync("api/BasegoodSize/add", testSize);
        await client.PostAsJsonAsync("api/Basegoodtype/add", testType);
        await client.PostAsJsonAsync("api/Basegood/add", testGood);
    }
    //A First Step
    [Fact]
    public async Task GivenThatProductForBaseGoodExists_WhenAddToCart_ThenAddThatProductToCart()
    {
        //ARRANGE
        int customerId = 1001;
        int BasegoodId = 1002;
        int allOtherId = 1235;
        int prodId = 1212;
        await setUpTest(customerId, BasegoodId, allOtherId);

        ProductAddonBasegood testProdAddBase = new ProductAddonBasegood() { Id = allOtherId, Addonid = null, Basegoodid = BasegoodId, Productid = prodId };
        Product testProduct = new Product() { Id = prodId, Description = "test product to add to cart", Ispublic = true, Productname = "A nice cake that we are testing" };
        await client.PostAsJsonAsync("api/Product/add", testProduct);
        await client.PostAsJsonAsync("api/ProductAddonBasegood/add", testProdAddBase);

        //ACT
        int testCartID = await client.GetFromJsonAsync<int>($"api/cart/addToCart/{customerId}/{BasegoodId}");
        Cart? testCart = await client.GetFromJsonAsync<Cart>($"api/cart/get/{testCartID}");

        //ASSERT
        Assert.NotNull(testCart);
        Assert.Equal(testCart.Id, testCartID);
        Assert.Equal(customerId, testCart.Customerid);
    }

    //THIS IS THE GOAL
    [Fact]
    public async Task GivenBaseGoodExists_WhenBaseGoodIsNotAProduct_ThenCreateProductWithBaseGood()
    {
        //ARRANGE
        int customerId = 1000;
        int BasegoodId = 1001;
        int allOtherId = 1234;
        await setUpTest(customerId, BasegoodId, allOtherId);

        //ACT
        int testCartID = await client.GetFromJsonAsync<int>($"api/cart/addToCart/{customerId}/{BasegoodId}");
        Cart? testCart = await client.GetFromJsonAsync<Cart>($"api/cart/get/{testCartID}");

        //ASSERT
        Assert.NotNull(testCart);
        Assert.Equal(testCart.Id, testCartID);
        Assert.Equal(customerId, testCart.Customerid);
    }







    ////THESE ARE OLD TESTS WE PROBABLY WANT TO MAKE API CALLS THAT ARE MORE SPECIFIC THAN THAT.

    //[Fact]
    //public async Task CanAddFinishedProductToCart()
    //{
    ///TODO this is the big test or the goal
    //// ARRANGE
    //CartLogic testCart = new CartLogic(client);

    //Basegood testGood = new Basegood()
    //{
    //    Id = 2000,
    //};
    ////Product testProduct = new Product()
    ////{
    ////    Id = 4000
    ////};
    ////ProductAddonBasegood finishedProduct = new ProductAddonBasegood()
    ////{
    ////    Id = 3000,
    ////    Productid = 4000
    ////};
    //Customer testCustomer = new Customer()
    //{
    //    Id = 5000
    //};

    ////Act
    //Product productInCart = await testCart.addBaseGoodAsync(testGood, testCustomer);
    //List<Cart>? carts = await client.GetFromJsonAsync<List<Cart>>("api/cart/getall");
    //Cart cart = carts.FirstOrDefault(c => c.Productid == productInCart.Id);
    ////Cart cart = await client.GetFromJsonAsync<Cart>("api/Cart/get")

    ////Assert
    //Assert.NotNull(cart);

    ////Assert.NotNull(client);
    //}

    //[Fact]
    //public async Task GivenBaseGoodExists_WhenBaseGoodIsNotALoneProduct_ThenCreateProductWithBaseGood()
    //{
    //    //TODO
    //}

    [Fact]
    public async Task CanCheckIfProductIsALoneProduct()
    {
        // CartLogic testCart = new CartLogic(client);
        Product testProduct = new Product() { Id = 1000 };
        Basegood testgood = new Basegood() { Id = 2000 };
        ProductAddonBasegood link = new ProductAddonBasegood()
        {
            Id = 3000,
            Productid = testProduct.Id,
            Basegoodid = testgood.Id
        };

        await client.PostAsJsonAsync("api/basegood/add", testgood);
        await client.PostAsJsonAsync("api/product/add", testProduct);
        await client.PostAsJsonAsync("api/productAddonBasegood/add", link);


        // int result = await testCart.FindProductForSingleBaseGoodAsync(testgood.Id);
        // Assert.Equal(testProduct.Id, result);
    }


    [Fact]
    public async Task WhenGivenNonExistingBaseGood_ThenWillThrowException()
    {
        Customer testCustomer = new Customer() { Id = 23456 };
        Basegood basegood = new Basegood() { Id = 1234567890 };
        // CartLogic testCart = new CartLogic(client);
        //await Assert.ThrowsAsync<InputFormatterException>(async () =>
        //{
        //    await testCart.addBaseGoodAsync(basegood, testCustomer);
        //});
    }
}