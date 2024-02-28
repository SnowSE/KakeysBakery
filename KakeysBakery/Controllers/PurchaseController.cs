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
    public async Task<List<Purchase>> GetAddonsAsync()
    {
        return await purchaseService.GetPurchaseListAsync();
    }

    [HttpPost("add")]
    public async Task CreateAddOnAsync(Purchase addon)
    {
        await purchaseService.CreatePurchaseAsync(addon);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteAddOnAsync(int id)
    {
        await purchaseService.DeletePurchaseAsync(id);
    }

    [HttpPatch("update")]
    public async Task UpdateAddonAsync(Purchase addon)
    {
        await purchaseService.UpdatePurchaseAsync(addon);
    }
}
