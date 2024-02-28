using KakeysBakery.Services;
using KakeysBakeryClassLib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class basegoodController : ControllerBase
{
    private readonly IBaseGoodService baseGoodService;
    public basegoodController(IBaseGoodService service)
    {
        baseGoodService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Basegood>> GetBasegoodsAsync()
    {
        return await baseGoodService.GetBaseGoodListAsync();
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
