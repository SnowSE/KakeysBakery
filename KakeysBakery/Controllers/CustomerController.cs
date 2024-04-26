using KakeysBakery.Services;

using KakeysSharedLib.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace KakeysBakery.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService CustomerService;
    public CustomerController(ICustomerService service)
    {
        CustomerService = service;
    }

    [HttpGet("getall")]
    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await CustomerService.GetCustomerListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetCustomerAsync(int id)
    {
        var prod = await CustomerService.GetCustomerAsync(id);
        if (prod == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(prod); // Return the addon if found
    }

    [HttpGet("get_by_name/{forname}")]
    public async Task<IActionResult> GetCustomerAsync(string forname)
    {
        var prod = await CustomerService.GetCustomerByNameAsync(forname);
        if (prod == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(prod); // Return the addon if found
    }

    [HttpGet("get_by_email/{email}")]
    public async Task<IActionResult> GetCustomerAsyncByEmail(string email)
    {
        var prod = await CustomerService.GetCustomerByEmail(email);
        if (prod == null)
        {
            return NotFound(); // Return 404 Not Found status
        }
        return Ok(prod); // Return the addon if found
    }

    [HttpPost("add")]
    public async Task CreateCustomerAsync([FromBody] Customer customer)
    {
        await CustomerService.CreateCustomerAsync(customer);
    }

    [HttpPatch("update")]
    public async Task UpdateCustomerAsync(Customer customer)
    {
        await CustomerService.UpdateCustomerAsync(customer);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteCustomerAsync(int id)
    {
        await CustomerService.DeleteCustomerAsync(id);
    }
}