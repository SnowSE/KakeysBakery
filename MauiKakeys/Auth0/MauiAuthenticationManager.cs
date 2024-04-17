using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysSharedLib.Data;
using KakeysSharedLib.OAuth;

namespace MauiKakeys.Auth0;

public class MauiAuthenticationManager : IAuthenticationManager
{
    public Task<Customer> CreateUser()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUserEmail()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetUserFromEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserLoggedIn()
    {
        throw new NotImplementedException();
    }
}