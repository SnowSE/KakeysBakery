using KakeysBakery.Services;

using KakeysBakeryClassLib.Services.Interfaces;

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

    [HttpGet("get/{selectedTypeId}/{typeId}")]
    public async Task<IActionResult> GetProductAddonBasegoodAsync2(int selectedTypeId, int typeId)
    {
        var pab = await productAddonBasegoodService.GetProductAddonBasegoodAsync(selectedTypeId, typeId);
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