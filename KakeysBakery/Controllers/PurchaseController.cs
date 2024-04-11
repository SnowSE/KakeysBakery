using KakeysBakery.Services;

using KakeysBakeryClassLib.Data;
using KakeysBakeryClassLib.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService purchaseService;
    public PurchaseController(IPurchaseService PurchaseService)
    {
        purchaseService = PurchaseService;
    }

    [HttpGet("getall")]
    public async Task<List<Purchase>> GetPurchaseListAsync()
    {
        return await purchaseService.GetPurchaseListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetPurchaseAsync(int id)
    {
        var addon = await purchaseService.GetPurchaseAsync(id);
        if (addon == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(addon); // Return the addon if found
    }

    [HttpPost("add")]
    public async Task CreatePurchaseAsync([FromBody] Purchase addon)
    {
        await purchaseService.CreatePurchaseAsync(addon);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeletePurchaseAsync(int id)
    {
        await purchaseService.DeletePurchaseAsync(id);
    }

    [HttpPatch("update")]
    public async Task UpdatePurchaseAsync(Purchase addon)
    {
        await purchaseService.UpdatePurchaseAsync(addon);
    }
}