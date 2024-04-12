using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysSharedLib.Data;

namespace KakeysSharedLib.Services.Interfaces;

public interface ICustomerRoleService
{
    public Task<List<CustomerRole>> GetCustomerRoleListAsync();
    public Task<CustomerRole?> GetCustomerRoleAsync(int id);
    public Task CreateCustomerRoleAsync(CustomerRole customerRole);
    public Task DeleteCustomerRoleAsync(int customerRoleId);
    public Task UpdateCustomerRoleAsync(CustomerRole customerRole);
}