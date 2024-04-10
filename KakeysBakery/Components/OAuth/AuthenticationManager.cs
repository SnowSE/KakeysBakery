﻿using KakeysBakeryClassLib.OAuth;

using Microsoft.AspNetCore.Components.Authorization;

namespace KakeysBakery.Components.OAuth;

public class AuthenticationManager(Task<AuthenticationState>? authState) : IAuthenticationManager
{
    private readonly Task<AuthenticationState>? authenticationState = authState;
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

    public async Task<bool> IsUserLoggedIn()
    {
        await GetAuthState();

        if (state is null) { return false; }
        if (state.User.Identity is null) { return false; }

        string email = await GetUserEmail();
        Customer = await GetUserFromEmail(email);

        return true;
    }

    public async Task<Customer> GetUserFromEmail(string email)
    {
        Customer? result = null;
        try
        {
            using var client = new HttpClient();
            result = await client.GetFromJsonAsync<Customer>($"api/customer/get_by_email/{email}");
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
            Forename = state!.User!.Identity!.Name,
            Email = state.User.Claims
                        .Where(c => c.Type.Contains("emailaddress"))
                        .FirstOrDefault()!.Value
        };

        using var client = new HttpClient();
        await client.PostAsJsonAsync("api/customer/add", user);

        return user;
    }

    public async Task<string> GetUserEmail()
    {
        await GetAuthState();
        if (state is null) { throw new NullReferenceException("Error: User is not logged in!"); }

        var user = state.User.Claims
            .Where(c => c.Type.Contains("emailaddress"))
            .FirstOrDefault();

        if (user is null) { throw new NullReferenceException("Error: User has no email!"); }
        return user.Value;
    }
}