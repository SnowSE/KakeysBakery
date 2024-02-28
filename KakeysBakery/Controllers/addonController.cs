using KakeysBakery.Services;
using KakeysBakeryClassLib.Data;
using Microsoft.AspNetCore.Mvc;
namespace KakeysBakery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class addonController : ControllerBase
{
    private readonly IAddonService addonService;
    public addonController(IAddonService AddOnService)
    {
        addonService = AddOnService;
    }

    [HttpGet("getall")]
    public async Task<List<Addon>> GetAddonsAsync()
    {
        return await addonService.GetAddonListAsync();
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
