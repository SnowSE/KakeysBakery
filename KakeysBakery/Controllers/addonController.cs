using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddonController : ControllerBase
{
    private readonly IAddonService addonService;
    public AddonController(IAddonService AddOnService)
    {
        addonService = AddOnService;
    }

    [HttpGet("getall")]
    public async Task<List<Addon>> GetAddonsAsync()
    {
        return await addonService.GetAddonListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAddonAsync(int id)
    {
        var addon = await addonService.GetAddonAsync(id);
        if (addon == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(addon); // Return the addon if found
    }


    [HttpPost("add")]
    public async Task CreateAddOnAsync(Addon addon)
    {
        await addonService.CreateAddOnAsync(addon);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteAddOnAsync(int id)
    {
        await addonService.DeleteAddOnAsync(id);
    }

    [HttpPatch("update")]
    public async Task UpdateAddonAsync(Addon addon)
    {
        await addonService.UpdateAddOnAsync(addon);
    }
}
