using KakeysBakery.Services;
using KakeysBakeryClassLib.Services.Interfaces;
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
    public async Task<Customer?> GetCustomerAsync(int id)
    {
        var prod = await CustomerService.GetCustomerAsync(id);
        return prod;
    }

    [HttpGet("get_by_name/{forname}/{surname}")]
    public async Task<Customer?> GetCustomerAsync(string forname, string surname)
    {
        var prod = await CustomerService.GetCustomerAsync(forname, surname);
        return prod;
    }

	[HttpGet("get_by_email/{email}")]
	public async Task<Customer?> GetCustomerAsyncByEmail(string email)
	{
		var prod = await CustomerService.GetCustomerByEmail(email);
        return prod;
    }

	[HttpPost("add")]
    public async Task CreateCustomerAsync(Customer customer)
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
