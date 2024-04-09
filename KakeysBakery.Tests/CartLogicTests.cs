using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

//using KakeysBakery.Services.nonDBServices;

using Microsoft.AspNetCore.Mvc.Formatters;

namespace KakeysBakeryTests;

public class CartLogicTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public CartLogicTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();

    }

    // 

    //THIS IS THE GOAL
    [Fact]
    public async Task GivenBaseGoodExists_WhenBaseGoodIsNotAProduct_ThenCreateProductWithBaseGood()
    {
        //ARRANGE
        int customerId = 1000;
        int BasegoodId = 1001;
        Customer testCustomer = new Customer()
        {
            Id = 5000
        };
        await client.PostAsJsonAsync("api/Cutomer/add",testCustomer);
        Basegoodflavor testflavor = new Basegoodflavor() { Id=1234, Flavorname="test Flavor Name"};
        BasegoodSize testSize = new BasegoodSize() { Id=1234, Size="test pan size"};
        Basegoodtype testType = new Basegoodtype() { Id=1234, Basegood="a test cake"};
        Basegood testGood = new Basegood() { Id = 1234, Flavorid = 1234, Goodsize=1234,Pastryid=1234};
        await client.PostAsJsonAsync("api/Basegoodflavor/add", testflavor);
        await client.PostAsJsonAsync("api/BasegoodSize/add", testSize);
        await client.PostAsJsonAsync("api/Basegoodtype/add", testType);
        await client.PostAsJsonAsync("api/Basegood/add", testGood);

        //ACT
        int testCartID = await client.GetFromJsonAsync<int>($"api/cart/addToCart/{customerId}/{BasegoodId}");
        Cart? testCart = await client.GetFromJsonAsync<Cart>($"api/cart/get/{testCartID}");

        Assert.NotNull(testCart );
        Assert.Equal( testCart.Id, testCartID );
        Assert.Equal(testCustomer.Id, testCart.Customerid );        
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