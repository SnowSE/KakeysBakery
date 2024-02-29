﻿using KakeysBakeryTests;
using KakeysBakeryClassLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KakeysBakeryTests;

public class ProductTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public ProductTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public void CanPassATest()
    {
        Assert.Equal(1, 1);
    }

    [Fact]
    public async Task Get_ProductList()
    {
        // ARRANGE
        Basegood basegood = new Basegood();
        basegood.Id = 1;

        Product testProduct = new()
        {
            Id = 245,
            Basegoodid = 1,
            Description = "Test",
            Productname = "TestName",
        };

        await client.PostAsJsonAsync("api/basegood/add", basegood);
        await client.PostAsJsonAsync("api/Product/add", testProduct);

        // ACT
        List<Product>? result = await client.GetFromJsonAsync<List<Product>>("api/Product/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_Product_ById()
    {
        // ARRANGE
        Basegood basegood = new Basegood();
        basegood.Id = 1;
        Product testProduct = new()
        {
            Id = 245,
            Basegoodid = 1,
            Description = "Test",
            Productname = "TestName",
        };
        await client.PostAsJsonAsync("api/basegood/add", basegood);
        await client.PostAsJsonAsync("api/Product/add", testProduct);

        // ACT
        Product? result = await client.GetFromJsonAsync<Product>($"api/Product/get/{testProduct.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testProduct.Description, result.Description);
        Assert.Equal(testProduct.Id, result.Id);
    }

    [Fact]
    public async Task Create_Product()
    {
        // ARRANGE
        Basegood basegood = new Basegood();
        basegood.Id = 1;
        Product testProduct = new()
        {
            Id = 255,
            Basegoodid = 1,
            Description = "Test",
            Productname = "TestName",
        };

        // ACT
        await client.PostAsJsonAsync("api/basegood/add", basegood);
        await client.PostAsJsonAsync("api/Product/add", testProduct);
        Product? result = await client.GetFromJsonAsync<Product>($"api/Product/get/{testProduct.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testProduct.Description, result.Description);
        Assert.Equal(testProduct.Id, result.Id);
    }

    [Fact]
    public async Task Edit_Product()
    {
        // ARRANGE
        Basegood basegood = new Basegood();
        basegood.Id = 1;
        Product testProduct = new()
        {
            Id = 245,
            Basegoodid = 1,
            Description = "Test",
            Productname = "TestName",
        };
        await client.PostAsJsonAsync("api/basegood/add", basegood);
        await client.PostAsJsonAsync("api/Product/add", testProduct);

        // ACT
        testProduct.Description = "some new description";
        await client.PatchAsJsonAsync("api/Product/update", testProduct);

        Product? result = await client.GetFromJsonAsync<Product>($"api/Product/get/{testProduct.Id}");


        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testProduct.Description, result.Description);
        Assert.Equal(testProduct.Id, result.Id);
    }

    [Fact]
    public async Task Delete_Product()
    {
        // ARRANGE
        Basegood basegood = new Basegood();
        basegood.Id = 1;
        Product testProduct = new()
        {
            Id = 245,
            Basegoodid = 1,
            Description = "Test",
            Productname = "TestName",
        };
        await client.PostAsJsonAsync("api/basegood/add", basegood);
        await client.PostAsJsonAsync("api/Product/add", testProduct);
        Product temp = await client.GetFromJsonAsync<Product>($"api/Product/get/{testProduct.Id}");
        // Assert 
        Assert.NotNull(temp);
        // ACT
        await client.DeleteAsync($"api/Product/delete/{testProduct.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Addon>($"api/Product/get/{testProduct.Id}");
        });
    }
}
