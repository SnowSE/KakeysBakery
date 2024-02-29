using KakeysBakery.Services;
using KakeysBakeryClassLib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService ProductService;
    public ProductController(IProductService service)
    {
        ProductService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Product>> GetProductsAsync()
    {
        return await ProductService.GetProductListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        var prod = await ProductService.GetProductAsync(id);
        if (prod == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(prod); // Return the addon if found
    }

    [HttpGet("get_by_name/{name}")]
    public async Task<IActionResult> GetProductAsync(string name)
    {
        var prod = await ProductService.GetProductAsync(name);
        if (prod == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(prod); // Return the addon if found
    }

    [HttpPost("add")]
    public async Task CreateProductAsync(Product product)
    {
        await ProductService.CreateProductAsync(product);
    }

    [HttpPatch("update")]
    public async Task UpdateProductAsync(Product product)
    {
        await ProductService.UpdateProductAsync(product);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteProductAsync(int id)
    {
        await ProductService.DeleteProductAsync(id);
    }
}
