using KakeysBakery.Services;

using KakeysBakeryClassLib.Data;
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
        var basegood = await baseGoodService.GetBaseGoodAsync(id);
        if (basegood == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(basegood); // Return the addon if found
    }

    [HttpGet("get_from_type/{id}")]
    public async Task<List<Basegood>> GetBasegoodsFromTypeAsync(int typeId)
    {
        return await baseGoodService.GetBasegoodsFromTypeAsync(typeId);
    }

    [HttpGet("get_from_flavor/{id}/{flavorId}")]
    public async Task<Basegood?> GetBasegoodsFromFlavorAsync(int id, int flavorId)
    {
        return await baseGoodService.GetBaseGoodFromFlavorAsync(id, flavorId);
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