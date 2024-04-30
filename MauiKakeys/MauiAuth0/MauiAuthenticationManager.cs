using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using KakeysSharedLib.Data;
using KakeysSharedLib.OAuth;

using Microsoft.AspNetCore.Components.Authorization;

namespace MauiKakeys.MauiAuth0;

public class MauiAuthenticationManager(HttpClient client) : IAuthenticationManager
{
    private Task<AuthenticationState>? authenticationState = null;
    private AuthenticationState? state;
    public Customer? Customer { get; set; } = null;

    private async Task GetAuthState()
    {
        if (authenticationState == null)
        {
            return;
        }

        if (state is null)
        {
            state = await authenticationState;
        }
    }

    public async Task SetAuthState(Task<AuthenticationState>? authState)
    {
        authenticationState = authState;
        await GetAuthState();
    }

    public async Task<bool> IsUserLoggedIn()
    {
        await GetAuthState();

        if (state is null) { return false; }
        if (state.User.Identity is null) { return false; }
        if (state.User.Identity.IsAuthenticated is false) { return false; }

        string email = await GetUserEmail();
        Customer = await GetUserFromName(email);

        return true;
    }

    public async Task<Customer> GetUserFromName(string email)
    {
        Customer? result = null;
        try
        {
            result = await client.GetFromJsonAsync<Customer>($"api/customer/get_by_name/{email}");
        }
        catch { }

        if (result is null)
        {
            return await CreateUser();
        }

        return result;
    }

    public async Task<Customer> CreateUser()
    {
        await GetAuthState();
        if (state is null) { throw new NullReferenceException("Error: User is not logged in!"); }

        Customer user = new()
        {
            Forename = state!.User!.Identity!.Name
        };

        await client.PostAsJsonAsync("api/customer/add", user);

        return user;
    }

    public async Task<string> GetUserEmail()
    {
        await GetAuthState();
        if (state is null) { throw new NullReferenceException("Error: User is not logged in!"); }

        var user = state.User.Identity;
        //.Where(c => c.Contains("emailaddress"))
        //.FirstOrDefault();

        if (user is null) { throw new NullReferenceException("Error: User has no email!"); }
        return user.Name ?? "";
    }

    public Task<Customer> GetUserFromEmail(string email)
    {
        throw new NotImplementedException();
    }
}