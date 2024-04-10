using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysBakeryClassLib.Data;

using Microsoft.AspNetCore.Components.Authorization;

namespace KakeysBakeryClassLib.OAuth;

public interface IAuthenticationManager
{
    public Task<bool> IsUserLoggedIn();
    public Task<Customer> GetUserFromEmail(string email);
    public Task<Customer> CreateUser();
    public Task<string> GetUserEmail();
}