using KakeysSharedLib.OAuth;

using Microsoft.AspNetCore.Components.Authorization;

namespace KakeysBakery.Components.OAuth;

public class WebAuthenticationManager(HttpClient client) : IAuthenticationManager
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

        state = await authenticationState;
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
        Customer = await GetUserFromEmail(email);

        return true;
    }

    public async Task<Customer> GetUserFromEmail(string email)
    {
        Customer? result = null;
        try
        {
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