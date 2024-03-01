using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KakeysBakeryTests;

public class CustomerTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public CustomerTests(BakeryFactory Factory)
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
    public async Task Get_CustomerList()
    {
        // ARRANGE
        Customer testCustomer = new Customer()
        {
            Id = 6,
            Email = "test Email",
            Forename = "first",
            Surname = "last",
            Phone = "8018018888",
            Preferredcontact = "Text"
        };

        await client.PostAsJsonAsync("api/customer/add", testCustomer);

        // ACT
        List<Customer>? Customers = await client.GetFromJsonAsync<List<Customer>>("api/customer/getall");

        // ASSERT
        Assert.NotNull(Customers);
        Assert.NotEmpty(Customers);
    }

    [Fact]
    public async Task Get_Customer_ById()
    {
        // ARRANGE
        Customer testCustomer = new Customer()
        {
            Id = 4,
            Email = "test Email",
            Forename = "first",
            Surname = "last",
            Phone = "8018018888",
            Preferredcontact = "Text"
        };

        await client.PostAsJsonAsync("api/customer/add", testCustomer);

        // ACT
        Customer? result = await client.GetFromJsonAsync<Customer>($"api/customer/get/{testCustomer.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testCustomer.Forename, result.Forename);
        Assert.Equal(testCustomer.Surname, result.Surname);
        Assert.Equal(testCustomer.Id, result.Id);
        Assert.Equal(testCustomer.Phone, result.Phone);
        Assert.Equal(testCustomer.Preferredcontact, result.Preferredcontact);
    }

    [Fact]
    public async Task Get_Customer_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Customer>($"api/customer/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_Customer_ByName()
    {
        // ARRANGE
        Customer testCustomer = new Customer()
        {
            Id = 3,
            Email = "test Email",
            Forename = "Jeffrey",
            Surname = "aheie",
            Phone = "8018018888",
            Preferredcontact = "Text"
        };

        await client.PostAsJsonAsync("api/customer/add", testCustomer);

        // ACT
        Customer? result = await client.GetFromJsonAsync<Customer>($"api/customer/get_by_name/{testCustomer.Forename}/{testCustomer.Surname}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testCustomer.Forename, result.Forename);
        Assert.Equal(testCustomer.Surname, result.Surname);
        Assert.Equal(testCustomer.Id, result.Id);
        Assert.Equal(testCustomer.Phone, result.Phone);
        Assert.Equal(testCustomer.Preferredcontact, result.Preferredcontact);
    }

    [Fact]
    public async Task Get_Customer_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Customer>($"api/customer/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_Customer()
    {
        // ARRANGE
        Customer testCustomer = new Customer()
        {
            Id = 5,
            Email = "test Email",
            Forename = "first",
            Surname = "last",
            Phone = "8018018888",
            Preferredcontact = "Text"
        };

        // ACT
        await client.PostAsJsonAsync("api/customer/add", testCustomer);
        Customer? result = await client.GetFromJsonAsync<Customer>($"api/customer/get/{testCustomer.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testCustomer.Forename, result.Forename);
        Assert.Equal(testCustomer.Surname, result.Surname);
        Assert.Equal(testCustomer.Id, result.Id);
        Assert.Equal(testCustomer.Phone, result.Phone);
        Assert.Equal(testCustomer.Preferredcontact, result.Preferredcontact);
    }

    [Fact]
    public async Task Create_Customer_When_AlreadyExists()
    {
        // ARRANGE
        Customer testCustomer = new Customer()
        {
            Id = 5,
            Email = "test Email",
            Forename = "first",
            Surname = "last",
            Phone = "8018018888",
            Preferredcontact = "Text"
        };

        await client.PostAsJsonAsync("api/customer/add", testCustomer);

        // ASSERT
        try
        {
            await client.PostAsJsonAsync("api/customer/add", testCustomer);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_Customer()
    {
        // ARRANGE
        Customer testCustomer = new Customer()
        {
            Id = 1,
            Email = "test Email",
            Forename = "first",
            Surname = "last",
            Phone = "8018018888",
            Preferredcontact = "Text"
        };

        await client.PostAsJsonAsync("api/customer/add", testCustomer);

        // ACT
        testCustomer.Surname = "Edited Test Married Name";
        await client.PatchAsJsonAsync("api/customer/update", testCustomer);
        Customer? result = await client.GetFromJsonAsync<Customer>($"api/customer/get/{testCustomer.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testCustomer.Surname, result.Surname);
        Assert.Equal(testCustomer.Id, result.Id);
    }

    [Fact]
    public async Task Delete_Customer()
    {
        // ARRANGE
        Customer testCustomer = new Customer()
        {
            Id = 2,
            Email = "test Email",
            Forename = "first",
            Surname = "last",
            Phone = "8018018888",
            Preferredcontact = "Text"
        };

        await client.PostAsJsonAsync("api/customer/add", testCustomer);

        // Assert
        Assert.NotNull(await client.GetFromJsonAsync<Customer>($"api/customer/get/{testCustomer.Id}"));

        // ACT
        await client.DeleteAsync($"api/customer/delete/{testCustomer.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Customer>($"api/customer/get/{testCustomer.Id}");
        });
    }

    [Fact]
    public async Task Delete_Customer_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/customer/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}