using KakeysBakery.Services;

using KakeysBakeryClassLib.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PurchaseProductController : ControllerBase
{
    private readonly IPurchaseProductService purchaseProductService;
    public PurchaseProductController(IPurchaseProductService service)
    {
        purchaseProductService = service;
    }

    [HttpGet("getall")]
    public async Task<List<PurchaseProduct>> GetPurchaseProductsAsync()
    {
        return await purchaseProductService.GetPurchaseProductListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetPurchaseProductAsync(int id)
    {
        var addon = await purchaseProductService.GetPurchaseProductAsync(id);
        if (addon == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(addon); // Return the addon if found
    }


    [HttpPost("add")]
    public async Task CreatePurchaseProductAsync(PurchaseProduct purchaseProduct)
    {
        await purchaseProductService.CreatePurchaseProductAsync(purchaseProduct);
    }

    [HttpPatch("update")]
    public async Task UpdatePurchaseProductAsync(PurchaseProduct purchaseProduct)
    {
        await purchaseProductService.UpdatePurchaseProductAsync(purchaseProduct);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeletePurchaseProductAsync(int id)
    {
        await purchaseProductService.DeletePurchaseProductAsync(id);
    }
}