using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]

public class CustomerRoleController : ControllerBase
{
    private readonly ICustomerRoleService customerRoleService;
    public CustomerRoleController(ICustomerRoleService service)
    {
        customerRoleService = service;
    }

    [HttpGet("getall")]
    public async Task<List<CustomerRole>> GetCustomerRolesAsync()
    {
        return await customerRoleService.GetCustomerRoleListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetCustomerRoleAsync(int id)
    {
        var customerRole = await customerRoleService.GetCustomerRoleAsync(id);
        if (customerRole == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(customerRole); // Return the customerRole if found
    }


    [HttpPost("add")]
    public async Task CreateCustomerRoleAsync([FromBody] CustomerRole customerRole)
    {
        await customerRoleService.CreateCustomerRoleAsync(customerRole);
    }

    [HttpPatch("update")]
    public async Task UpdateCustomerRoleAsync(CustomerRole customerRole)
    {
        await customerRoleService.UpdateCustomerRoleAsync(customerRole);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteCustomerRoleAsync(int id)
    {
        await customerRoleService.DeleteCustomerRoleAsync(id);
    }
}