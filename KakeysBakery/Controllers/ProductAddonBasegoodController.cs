using KakeysBakery.Services;

using KakeysSharedLib.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductAddonBasegoodController : ControllerBase
{
    private readonly IProductAddonBasegoodService productAddonBasegoodService;
    public ProductAddonBasegoodController(IProductAddonBasegoodService service)
    {
        productAddonBasegoodService = service;
    }

    [HttpGet("getall")]
    public async Task<List<ProductAddonBasegood>> GetProductAddonBasegoodsAsync()
    {
        return await productAddonBasegoodService.GetProductAddonBasegoodListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetProductAddonBasegoodAsync(int id)
    {
        var pab = await productAddonBasegoodService.GetProductAddonBasegoodAsync(id);
        if (pab == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(pab); // Return the addon if found
    }

    [HttpGet("get_by_productId/{productId}")]
    public async Task<IActionResult> GetProductAddonBasegoodByProductIdAsync(int productId)
    {
        var pab = await productAddonBasegoodService.GetProductAddonBasegoodByProductIdAsync(productId);
        if (pab == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(pab); // Return the addon if found
    }

    [HttpGet("get/{flavorId}/{typeId}")]
    public async Task<IActionResult> GetProductAddonBasegoodAsync2(int flavorId, int typeId)
    {
        var pab = await productAddonBasegoodService.GetProductAddonBasegoodAsync(flavorId, typeId);
        if (pab == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(pab); // Return the addon if found
    }


    [HttpPost("add")]
    public async Task CreateProductAddonBasegoodAsync([FromBody] ProductAddonBasegood productAddonBasegood)
    {
        await productAddonBasegoodService.CreateProductAddonBasegoodAsync(productAddonBasegood);
    }

    [HttpPatch("update")]
    public async Task UpdateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood)
    {
        await productAddonBasegoodService.UpdateProductAddonBasegoodAsync(productAddonBasegood);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteProductAddonBasegoodAsync(int id)
    {
        await productAddonBasegoodService.DeleteProductAddonBasegoodAsync(id);
    }
}