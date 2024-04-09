using KakeysBakeryClassLib.OAuth;
using Microsoft.AspNetCore.Components.Authorization;

namespace KakeysBakery.Components.OAuth;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly Task<AuthenticationState>? authenticationState;
    private readonly HttpClient client;
    public Customer? Customer { get; set; } = null;

    public AuthenticationManager(Task<AuthenticationState>? authState, HttpClient httpClient)
    {
        authenticationState = authState;
        client = httpClient;
    }

    public async Task<bool> IsUserLoggedIn()
    {
        if (authenticationState is null) { return false; }

        var state = await authenticationState;
        if (state is null) { return false; }
        if (state.User.Identity is null) { return false; }

        string? email = GetUserEmail(state);
        if (email == null) { return false; }


        Customer = await GetUserFromEmail(email, state);
        return true;
    }

    public async Task<Customer> GetUserFromEmail(string email, AuthenticationState state)
    {
        Customer? result = null;
        try
        {
            result = await client.GetFromJsonAsync<Customer>($"api/customer/get_by_email/{email}");
        }
        catch { }

        if (result is null)
        {
            return await CreateUser(state);
        }

        return result;
    }

    public async Task<Customer> CreateUser(AuthenticationState state)
    {
        Customer User = new Customer()
        {
            Forename = state!.User!.Identity!.Name,
            Email = state.User.Claims
                        .Where(c => c.Type.Contains("emailaddress"))
                        .FirstOrDefault()!.Value
        };

        await client.PostAsJsonAsync("api/customer/add", User);

        return User;
    }

    public string? GetUserEmail(AuthenticationState state)
    {
        var user = state.User.Claims
            .Where(c => c.Type.Contains("emailaddress"))
            .FirstOrDefault();

        if (user is null) { return null; }
        return user.Value;
    }
}