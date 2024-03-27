using KakeysBakeryClassLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface ICustomerService
{
    public Task<List<Customer>> GetCustomerListAsync();
    public Task<Customer?> GetCustomerAsync(int customerId);
    public Task<Customer?> GetCustomerAsync(string forname, string surname);
    public Task CreateCustomerAsync(Customer customer);
    public Task DeleteCustomerAsync(int customerId);
    public Task UpdateCustomerAsync(Customer customer);
    public Task<Customer?> GetCustomerByEmail(string email);
}
