using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class cartController : ControllerBase
{
    private readonly ICartService cartService;
    public cartController(ICartService service)
    {
        cartService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Cart>> GetcartsAsync()
    {
        return await cartService.GetCartListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetcartAsync(int id)
    {
        var cart = await cartService.GetCartAsync(id);
        if (cart == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(cart); // Return the addon if found
    }

    [HttpGet("get_from_email/{email}")]
    public async Task<IActionResult> GetcartAsync(string email)
    {
        var cart = await cartService.GetCartFromEmailAsync(email);
        if (cart == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(cart); // Return the addon if found
    }

    [HttpPost("add")]
    public async Task CreatecartAsync(Cart cart)
    {
        await cartService.CreateCartAsync(cart);
    }

    [HttpPatch("update")]
    public async Task UpdatecartAsync(Cart cart)
    {
        await cartService.UpdateCartAsync(cart);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeletecartAsync(int id)
    {
        await cartService.DeleteCartAsync(id);
    }

    [HttpGet("addToCart/{customerId}/{BasegoodId}")]
    public async Task<int> AddTocartAsync(int customerId, int BasegoodId)
    {
        try
        {
            int cartId = await cartService.AddToCustomersCart(customerId, BasegoodId);
            return cartId; // Return the addon if found
        }
        catch { return -2; }
    }
}