using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class CustomerRoleService : ICustomerRoleService
{
    readonly PostgresContext _context;
    public CustomerRoleService(PostgresContext context)
    {
        _context = context;
    }
    public async Task CreateCustomerRoleAsync(CustomerRole customerRole)
    {
        _context.CustomerRoles.Add(customerRole);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomerRoleAsync(int customerRoleID)
    {
        CustomerRole? customerRole = await _context.CustomerRoles.FirstOrDefaultAsync(a => a.Id == customerRoleID);
        if (customerRole != null)
        {
            _context.CustomerRoles.Remove(customerRole);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<CustomerRole>> GetCustomerRoleListAsync()
    {
        return await _context.CustomerRoles.ToListAsync() ?? [];
    }

    public async Task<CustomerRole?> GetCustomerRoleAsync(int id)
    {
        return await _context.CustomerRoles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateCustomerRoleAsync(CustomerRole customerRole)
    {
        _context.CustomerRoles.Update(customerRole);
        await _context.SaveChangesAsync();
    }
}