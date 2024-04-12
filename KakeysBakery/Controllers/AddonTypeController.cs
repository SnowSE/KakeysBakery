using KakeysBakery.Services;

using KakeysSharedLib.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AddonTypeController : ControllerBase
{
    private readonly IAddonTypeService baseGoodService;
    public AddonTypeController(IAddonTypeService service)
    {
        baseGoodService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Addontype>> GetAddonTypesAsync()
    {
        return await baseGoodService.GetAddonTypeListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAddonTypeAsync(int id)
    {
        var addonType = await baseGoodService.GetAddonTypeAsync(id);
        if (addonType == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(addonType); // Return the addon if found
    }


    [HttpPost("add")]
    public async Task CreateAddonTypeAsync([FromBody] Addontype addonType)
    {
        await baseGoodService.CreateAddonTypeAsync(addonType);
    }

    [HttpPatch("update")]
    public async Task UpdateAddonTypeAsync(Addontype addonType)
    {
        await baseGoodService.UpdateAddonTypeAsync(addonType);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteAddonTypeAsync(int id)
    {
        await baseGoodService.DeleteAddonTypeAsync(id);
    }
}