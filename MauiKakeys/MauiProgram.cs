﻿using KakeysSharedLib.OAuth;

using MauiKakeys.Auth0;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MauiKakeys
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-zas6rizyxopiwv2b.us.auth0.com",
                ClientId = "xUuj4xt0Pn4wLdompKNjM3suZZKx9fdC",
                Scope = "openid profile",
                RedirectUri = "myapp://callback"
            }));
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazorBootstrap();

            builder.Services.AddScoped(o =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://kakeysbakery20240319120850.azurewebsites.net")
                };
                return client;
            });

            builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthenticationManager, MauiAuthenticationManager>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}