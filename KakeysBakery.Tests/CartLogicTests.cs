using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using KakeysBakery.Services.nonDBServices;

using Microsoft.AspNetCore.Mvc.Formatters;

namespace KakeysBakeryTests;

public class CartLogicTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public CartLogicTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();

    }


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
        CartLogic testCart = new CartLogic(client);
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


        int result = await testCart.FindProductForSingleBaseGoodAsync(testgood.Id);
        Assert.Equal(testProduct.Id, result);
    }


    [Fact]
    public async Task WhenGivenNonExistingBaseGood_ThenWillThrowException()
    {
        Customer testCustomer = new Customer() { Id = 23456 };
        Basegood basegood = new Basegood() { Id = 1234567890 };
        CartLogic testCart = new CartLogic(client);
        await Assert.ThrowsAsync<InputFormatterException>(async () =>
        {
            await testCart.addBaseGoodAsync(basegood, testCustomer);
        });
    }
}