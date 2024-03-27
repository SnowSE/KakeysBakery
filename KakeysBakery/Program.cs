using KakeysBakery.Components;
using KakeysBakery.Data;
using KakeysBakery.Services;
using KakeysBakeryClassLib.Services.Interfaces;
using KakeysBakeryClassLib.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using KakeysBakery.Components.PayPalAuth;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;        
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using KakeysBakery.Components.AuthenticationStateSyncer;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

//For Rendering the authorization state
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IAddonService, AddOnService>();
builder.Services.AddScoped<IBaseGoodService, BaseGoodService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPurchaseProductService, PurchaseProductService>();
builder.Services.AddScoped<IProductAddonBasegoodService, ProductAddonBasegoodService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IBaseGoodTypeService, BaseGoodTypeService>();
builder.Services.AddScoped<IBaseGoodFlavorService, BaseGoodFlavorService>();
builder.Services.AddScoped<IAddonTypeService, AddonTypeService>();
builder.Services.AddScoped<IAddonFlavorService, AddonFlavorService>();
builder.Services.AddScoped<IUserroleService, UserroleService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICustomerRoleService, CustomerRoleService>();

builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0Domain"];
        options.ClientId = builder.Configuration["Auth0ClientId"];
		options.Scope = "openid profile email";
	});

builder.Services.AddHttpClient();

builder.Services.AddScoped<IPayPalAuthentication,PayPalAuthentication>();

builder.Services.AddBlazorBootstrap();

builder.Services.AddControllers();
builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.Configuration["db"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

//for OAuth
app.MapGet("/Account/Login", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirectUri)
            .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirectUri)
            .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();

public partial class Program { };