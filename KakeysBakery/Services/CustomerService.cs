
using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;
public class CustomerService : ICustomerService
{
    private readonly PostgresContext _context;
    public CustomerService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int productId)
    {
        Customer? customer = _context.Customers.FirstOrDefault(b => b.Id == productId);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Customer>> GetCustomerListAsync()
    {
        return await _context.Customers.ToListAsync() ?? [];
    }

    public async Task<Customer?> GetCustomerAsync(int id)
    {
        return await _context.Customers
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task<Customer?> GetCustomerAsync(string forname, string surname)
    {
        return await _context.Customers
                .Where(b => b.Forename == forname && b.Surname == surname)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<Customer?> GetCustomerByEmail(string email)
    {
        return await _context.Customers.Where(b => b.Email == email).FirstOrDefaultAsync();
    }
}