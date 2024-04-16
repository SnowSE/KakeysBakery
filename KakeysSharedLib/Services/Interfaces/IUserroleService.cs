using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysSharedLib.Data;

namespace KakeysSharedLib.Services.Interfaces;

public interface IUserroleService
{
    public Task<List<Userrole>> GetUserroleListAsync();
    public Task<Userrole?> GetUserroleAsync(int id);
    public Task CreateUserroleAsync(Userrole userrole);
    public Task DeleteUserroleAsync(int userroleId);
    public Task UpdateUserroleAsync(Userrole userrole);
}