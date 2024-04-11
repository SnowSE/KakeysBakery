using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class CustomerRoleService : ICustomerRoleService
{
    readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public CustomerRoleService(KakeysBakeryClassLib.Data.PostgresContext context)
    {
        _context = context;
    }
    public async Task CreateCustomerRoleAsync(CustomerRole customerRole)
    {
        try
        {
            _context.CustomerRoles.Add(customerRole);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteCustomerRoleAsync(int customerRoleID)
    {
        try
        {
            CustomerRole? customerRole = await _context.CustomerRoles.FirstOrDefaultAsync(a => a.Id == customerRoleID);
            if (customerRole != null)
            {
                _context.CustomerRoles.Remove(customerRole);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
    }

    public async Task<List<CustomerRole>> GetCustomerRoleListAsync()
    {
        try
        {
            return await _context.CustomerRoles.ToListAsync();
        }
        catch { return new List<CustomerRole>(); }
    }

    public async Task<CustomerRole?> GetCustomerRoleAsync(int id)
    {
        return await _context.CustomerRoles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateCustomerRoleAsync(CustomerRole customerRole)
    {
        try
        {
            _context.CustomerRoles.Update(customerRole);
            await _context.SaveChangesAsync();
        }
        catch { }
    }
}