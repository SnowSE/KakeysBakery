using KakeysBakery.Services;

using KakeysBakeryClassLib.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BasegoodflavorController : ControllerBase
{
    private readonly IBaseGoodFlavorService baseGoodService;
    public BasegoodflavorController(IBaseGoodFlavorService service)
    {
        baseGoodService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Basegoodflavor>> GetBasegoodflavorsAsync()
    {
        return await baseGoodService.GetBaseGoodFlavorListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetBasegoodflavorAsync(int id)
    {
        var baseGoodFlavor = await baseGoodService.GetBaseGoodFlavorAsync(id);
        if (baseGoodFlavor == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(baseGoodFlavor); // Return the addon if found
    }


    [HttpPost("add")]
    public async Task CreateBaseGoodFlavorAsync(Basegoodflavor baseGoodFlavor)
    {
        await baseGoodService.CreateBaseGoodFlavorAsync(baseGoodFlavor);
    }

    [HttpPatch("update")]
    public async Task UpdateBaseGoodFlavorAsync(Basegoodflavor baseGoodFlavor)
    {
        await baseGoodService.UpdateBaseGoodFlavorAsync(baseGoodFlavor);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteBaseGoodFlavorAsync(int id)
    {
        await baseGoodService.DeleteBaseGoodFlavorAsync(id);
    }
}