using KakeysSharedLib.Data;

namespace KakeysSharedLib.Services.Interfaces;

public interface ICustomerService
{
    public Task<List<Customer>> GetCustomerListAsync();
    public Task<Customer?> GetCustomerAsync(int customerId);
    public Task<Customer?> GetCustomerByNameAsync(string forname);
    public Task CreateCustomerAsync(Customer customer);
    public Task DeleteCustomerAsync(int customerId);
    public Task UpdateCustomerAsync(Customer customer);
    public Task<Customer?> GetCustomerByEmail(string email);
}