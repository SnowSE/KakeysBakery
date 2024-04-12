using KakeysBakery.Services;

using KakeysSharedLib.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AddonflavorController : ControllerBase
{
    private readonly IAddonFlavorService addonFlavorService;
    public AddonflavorController(IAddonFlavorService service)
    {
        addonFlavorService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Addonflavor>> GetAddonflavorsAsync()
    {
        return await addonFlavorService.GetAddonFlavorListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAddonflavorAsync(int id)
    {
        var addonFlavor = await addonFlavorService.GetAddonFlavorAsync(id);
        if (addonFlavor == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(addonFlavor); // Return the addon if found
    }


    [HttpPost("add")]
    public async Task CreateAddonFlavorAsync([FromBody] Addonflavor addonFlavor)
    {
        await addonFlavorService.CreateAddonFlavorAsync(addonFlavor);
    }

    [HttpPatch("update")]
    public async Task UpdateAddonFlavorAsync(Addonflavor addonFlavor)
    {
        await addonFlavorService.UpdateAddonFlavorAsync(addonFlavor);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteAddonFlavorAsync(int id)
    {
        await addonFlavorService.DeleteAddonFlavorAsync(id);
    }
}