
using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;
public class CustomerService : ICustomerService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public CustomerService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateCustomerAsync(Customer customer)
    {
        try
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteCustomerAsync(int productId)
    {
        try
        {
            Customer? customer = _context.Customers.FirstOrDefault(b => b.Id == productId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<Customer>> GetCustomerListAsync()
    {
        try
        {
            return await _context.Customers.ToListAsync();
        }
        catch { return new List<Customer>(); }
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

    public Task UpdateCustomerAsync(Customer customer)
    {
        try
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<Customer?> GetCustomerByEmail(string email)
    {
        return await _context.Customers.Where(b => b.Email == email).FirstOrDefaultAsync();
    }
}