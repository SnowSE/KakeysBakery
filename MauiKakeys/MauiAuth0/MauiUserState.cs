using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;

namespace MauiKakeys.MauiAuth0;

public class MauiUserState
{
    private readonly AuthenticationStateProvider authProvider;
    public MauiUserState(AuthenticationStateProvider authProvider)
    {
        this.authProvider = authProvider;
        user = new ClaimsPrincipal();
    }
    private ClaimsPrincipal user;

    public ClaimsPrincipal User => user;

    public async Task Login()
    {
        await ((Auth0AuthenticationStateProvider)authProvider).LogInAsync();
    }

    public async Task Logout()
    {
        await Task.CompletedTask;
        ((Auth0AuthenticationStateProvider)authProvider).LogOut();
    }
}
