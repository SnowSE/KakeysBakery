using System.Net.Http.Json;

namespace KakeysBakeryTests.CRUD_Tests;

public class CartTests : IClassFixture<BakeryFactory>
{
    public HttpClient client { get; set; }
    public CartTests(BakeryFactory Factory)
    {
        client = Factory.CreateDefaultClient();
    }
    [Fact]
    public async Task Get_CartList()
    {
        // ARRANGE
        Cart testCart = new()
        {
            Id = 77,
            //  Customerid = 1
        };

        await client.PostAsJsonAsync("api/cart/add", testCart);

        // ACT
        List<Cart>? result = await client.GetFromJsonAsync<List<Cart>>("api/cart/getall");

        // ASSERT
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_Cart_ById()
    {
        // ARRANGE
        Cart testCart = new()
        {
            Id = 78
        };

        await client.PostAsJsonAsync("api/cart/add", testCart);

        // ACT
        Cart? result = await client.GetFromJsonAsync<Cart>($"api/cart/get/{testCart.Id}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testCart.Id, result.Id);
    }

    [Fact]
    public async Task Get_Cart_ByEmail()
    {
        // ARRANGE
        Customer customer = new()
        {
            Id = 1100,
            Email = "test@example.com"
        };
        
        Cart testCart = new()
        {
            Id = 1110,
            Customerid = customer.Id,
        };

        await client.PostAsJsonAsync("api/customer/add", customer);
        await client.PostAsJsonAsync("api/cart/add", testCart);

        // ACT
        Cart? result = await client.GetFromJsonAsync<Cart>($"api/cart/get_from_email/{customer.Email}");

        // ASSERT
        Assert.NotNull(result);

        Assert.Equal(testCart.Id, result.Id);
    }

    [Fact]
    public async Task Get_Cart_ById_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Cart>($"api/cart/get/{-1}");
        });
    }

    [Fact]
    public async Task Get_Cart_ByName_When_NotExists()
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Cart>($"api/cart/get_by_name/foo");
        });
    }

    [Fact]
    public async Task Create_Cart()
    {
        // ARRANGE
        Cart testCart = new()
        {
            Id = 80
        };

        // ACT
        await client.PostAsJsonAsync("api/cart/add", testCart);
        Cart? result = await client.GetFromJsonAsync<Cart>($"api/cart/get/{testCart.Id}");

        // Assert
        Assert.NotNull(result);

        Assert.Equal(testCart.Id, result.Id);
    }

    [Fact]
    public async Task Create_Cart_When_AlreadyExists()
    {
        // ARRANGE
        Cart existing = new()
        {
            Id = 101
        };

        // ACT
        await client.PostAsJsonAsync("api/cart/add", existing);

        // Assert
        try
        {
            await client.PostAsJsonAsync("api/cart/add", existing);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }

    [Fact]
    public async Task Edit_Cart()
    {
        // ARRANGE
        Cart testCart = new()
        {
            Id = 81
        };

        await client.PostAsJsonAsync("api/cart/add", testCart);

        // ACT
        //testCart.Cartname = "EditedTestName";
        await client.PatchAsJsonAsync("api/cart/update", testCart);

        Cart? result = await client.GetFromJsonAsync<Cart>($"api/cart/get/{testCart.Id}");


        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(testCart.Id, result.Id);
    }

    [Fact]
    public async Task Delete_Cart()
    {
        // ARRANGE
        Cart testCart = new()
        {
            Id = 82
        };

        await client.PostAsJsonAsync("api/cart/add", testCart);

        // ACT
        await client.DeleteAsync($"api/cart/delete/{testCart.Id}");

        // ASSERT
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await client.GetFromJsonAsync<Cart>($"api/cart/get/{testCart.Id}");
        });
    }

    [Fact]
    public async Task Delete_Cart_When_NotExists()
    {
        try
        {
            await client.DeleteAsync($"api/cart/delete/{-1}");
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got:" + ex.Message);
        }
    }
}