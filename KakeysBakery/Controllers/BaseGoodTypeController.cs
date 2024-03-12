using KakeysBakery.Services;
using KakeysBakeryClassLib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BasegoodtypeController : ControllerBase
{
    private readonly IBaseGoodTypeService baseGoodTypeService;
    public BasegoodtypeController(IBaseGoodTypeService service)
    {
        baseGoodTypeService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Basegoodtype>> GetBasegoodtypesAsync()
    {
        return await baseGoodTypeService.GetBaseGoodTypeListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetBaseGoodListAsync(int id)
    {
        var baseGoodTypeType = await baseGoodTypeService.GetBaseGoodTypeAsync(id);
        if (baseGoodTypeType == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(baseGoodTypeType); // Return the addon if found
    }


    [HttpPost("add")]
    public async Task CreateBaseGoodTypeAsync(Basegoodtype baseGoodType)
    {
        await baseGoodTypeService.CreateBaseGoodTypeAsync(baseGoodType);
    }

    [HttpPatch("update")]
    public async Task UpdateBaseGoodTypeAsync(Basegoodtype baseGoodType)
    {
        await baseGoodTypeService.UpdateBaseGoodTypeAsync(baseGoodType);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteBaseGoodTypeAsync(int id)
    {
        await baseGoodTypeService.DeleteBaseGoodTypeAsync(id);
    }
}