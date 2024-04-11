using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]

public class UserroleController : ControllerBase
{
    private readonly IUserroleService userroleService;
    public UserroleController(IUserroleService service)
    {
        userroleService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Userrole>> GetUserrolesAsync()
    {
        return await userroleService.GetUserroleListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetUserroleAsync(int id)
    {
        var userrole = await userroleService.GetUserroleAsync(id);
        if (userrole == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(userrole); // Return the userrole if found
    }


    [HttpPost("add")]
    public async Task CreateUserroleAsync([FromBody] Userrole userrole)
    {
        await userroleService.CreateUserroleAsync(userrole);
    }

    [HttpPatch("update")]
    public async Task UpdateUserroleAsync(Userrole userrole)
    {
        await userroleService.UpdateUserroleAsync(userrole);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteUserroleAsync(int id)
    {
        await userroleService.DeleteUserroleAsync(id);
    }
}