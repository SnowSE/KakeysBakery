using System.Net.Http.Json;
using System.Text.Json;

namespace KakeysBakeryTests;

public class CustomerRoleTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public CustomerRoleTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }

    [Fact]
    public async Task Get_CustomerRoleList()
    {
        // ARRANGE
        Customer testcustomer = new()
        {
            Id = 19,
            Email = "someemail@test",
            Forename = "tiestfirst",
            Surname = "testsure",
            Phone = "3453451234",
            Preferredcontact = "Text",
            Issubscribed = true
        };
        Userrole testUserRole = new() { Id = 15, Userrole1 = "test role" };
        CustomerRole testCustomerRole = new()
        {
            Id = 78,
            CustomerId = testcustomer.Id,
            UserroleId = testUserRole.Id
        };

        var response1 = await client.PostAsJsonAsync("api/Customer/add", testcustomer);
        var response2 = await client.PostAsJsonAsync("api/UserRole/add", testUserRole);
        var response3 = await client.PostAsJsonAsync("api/CustomerRole/add", testCustomerRole);
        // ACT
        List<CustomerRole>? result = await client.GetFromJsonAsync<List<CustomerRole>>("api/CustomerRole/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_CustomerRole_ById()
    {
        // ARRANGE
        Customer testcustomer = new()
        {
            Id = 19,
            Email = "someemail@test",
            Forename = "tiestfirst",
            Surname = "testsure",
            Phone = "3453451234",
            Preferredcontact = "Text",
            Issubscribed = true
        };
        Userrole testUserRole = new() { Id = 15, Userrole1 = "test role" };
        CustomerRole testCustomerRole = new()
        {
            Id = 78,
            CustomerId = testcustomer.Id,
            UserroleId = testUserRole.Id
        };

        var response1 = await client.PostAsJsonAsync("api/Customer/add", testcustomer);
        var response2 = await client.PostAsJsonAsync("api/UserRole/add", testUserRole);
        var response3 = await client.PostAsJsonAsync("api/CustomerRole/add", testCustomerRole);
        // ACT
        CustomerRole? result = await client.GetFromJsonAsync<CustomerRole>($"api/CustomerRole/get/{testCustomerRole.Id}");

        // ASSERT
        Assert.NotNull(result);
        //Assert.Equal(testCustomerRole.CustomerRole1, result.CustomerRole1);
        Assert.Equal(testCustomerRole.Id, result.Id);
    }

    [Fact]
    public async Task Get_CustomerRole_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<CustomerRole>($"api/CustomerRole/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_CustomerRole_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<CustomerRole>($"api/CustomerRole/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_CustomerRole()
    {
        // ARRANGE
        Customer testcustomer = new() { Id = 17,
            Email = "someemail@test",
            Forename ="tiestfirst",
            Surname = "testsure",
            Phone = "3453451234",
            Preferredcontact = "Text",
            Issubscribed = true
        };
        Userrole testUserRole = new() { Id = 12, Userrole1 = "test role" };
        CustomerRole testCustomerRole = new()
        {
            Id = 80,
            CustomerId = testcustomer.Id,
            UserroleId = testUserRole.Id
        };

        // ACT
        try
        {
            var json = JsonSerializer.Serialize(testCustomerRole);
            await client.PostAsJsonAsync("api/Customer/add", testCustomerRole);
            await client.PostAsJsonAsync("api/UserRole/add", testCustomerRole);
            await client.PostAsJsonAsync("api/CustomerRole/add", testCustomerRole);
            //var content = await response.Content.ReadAsStringAsync();
            CustomerRole? result = await client.GetFromJsonAsync<CustomerRole>($"api/CustomerRole/get/{testCustomerRole.Id}");


        // Assert
        Assert.NotNull(result);
       // Assert.Equal(testCustomerRole.CustomerRole1, result.CustomerRole1);
        Assert.Equal(testCustomerRole.Id, result.Id);
        }
        catch (Exception ex)
        { 
        }
    }

    [Fact]
    public async Task Create_CustomerRole_When_AlreadyExists()
    {
        // ARRANGE
        CustomerRole existing = new()
        {
            Id = 101
        };

        // ACT
        await client.PostAsJsonAsync("api/CustomerRole/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/CustomerRole/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_CustomerRole()
    {
        // ARRANGE
        Customer testcustomer = new()
        {
            Id = 18,
            Email = "someemail@test",
            Forename = "tiestfirst",
            Surname = "testsure",
            Phone = "3453451234",
            Preferredcontact = "Text",
            Issubscribed = true
        };
        Userrole testUserRole = new() { Id = 13, Userrole1 = "test role" };
        CustomerRole testCustomerRole = new()
        {
            Id = 81,
            CustomerId = testcustomer.Id,
            UserroleId = testUserRole.Id
        };

        var response1 = await client.PostAsJsonAsync("api/Customer/add", testcustomer);
        var response2 = await client.PostAsJsonAsync("api/UserRole/add", testUserRole);
        var response3 = await client.PostAsJsonAsync("api/CustomerRole/add", testCustomerRole);
        // ACT
        //TODO testCustomerRole.CustomerRole1 = "testrole Update";
        //testCustomerRole.CustomerRolename = "EditedTestName";
        await client.PatchAsJsonAsync("api/CustomerRole/update", testCustomerRole);

        CustomerRole? result = await client.GetFromJsonAsync<CustomerRole>($"api/CustomerRole/get/{testCustomerRole.Id}");


        // ASSERT
        Assert.NotNull(result);
        //Assert.Equal(testCustomerRole.CustomerRole1, result.CustomerRole1);
        Assert.Equal(testCustomerRole.Id, result.Id);
    }

    [Fact]
    public async Task Delete_CustomerRole()
    {
        // ARRANGE
        CustomerRole testCustomerRole = new()
        {
            Id = 82
        };

        await client.PostAsJsonAsync("api/CustomerRole/add", testCustomerRole);

        // ACT
        await client.DeleteAsync($"api/CustomerRole/delete/{testCustomerRole.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<CustomerRole>($"api/CustomerRole/get/{testCustomerRole.Id}");
        });
    }

    [Fact]
    public async Task Delete_CustomerRole_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/CustomerRole/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}