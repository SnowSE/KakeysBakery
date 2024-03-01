using KakeysBakery.Services;
using KakeysBakeryClassLib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BasegoodController : ControllerBase
{
    private readonly IBaseGoodService baseGoodService;
    public BasegoodController(IBaseGoodService service)
    {
        baseGoodService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Basegood>> GetBasegoodsAsync()
    {
        return await baseGoodService.GetBaseGoodListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetBasegoodAsync(int id)
    {
        var addon = await baseGoodService.GetBaseGoodAsync(id);
        if (addon == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(addon); // Return the addon if found
    }

    [HttpGet("get_by_name/{name}")]
    public async Task<IActionResult> GetBasegoodAsync(string name)
    {
        var addon = await baseGoodService.GetBaseGoodAsync(name);
        if (addon == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(addon); // Return the addon if found
    }

    [HttpPost("add")]
    public async Task CreateBaseGoodAsync(Basegood basegood)
    {
        await baseGoodService.CreateBaseGoodAsync(basegood);
    }

    [HttpPatch("update")]
    public async Task UpdateBaseGoodAsync(Basegood basegood)
    {
        await baseGoodService.UpdateBaseGoodAsync(basegood);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteBaseGoodAsync(int id)
    {
        await baseGoodService.DeleteBaseGoodAsync(id);
    }
}