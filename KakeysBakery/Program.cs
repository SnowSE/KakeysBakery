using System.Text.Json.Serialization;

using Auth0.AspNetCore.Authentication;

using KakeysBakery.Components;
using KakeysBakery.Components.AuthenticationStateSyncer;
using KakeysBakery.Components.OAuth;
using KakeysBakery.Services;

using KakeysSharedLib.OAuth;
using KakeysSharedLib.Pages;
using KakeysSharedLib.PayPalAuth;
using KakeysSharedLib.Services.Implementations;
using KakeysSharedLib.Services.Interfaces;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//For Rendering the authorization state
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();


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
builder.Services.AddScoped<IBasegoodSizeService, BaseGoodSizeService>();
builder.Services.AddScoped<IAuthenticationManager, WebAuthenticationManager>();

builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration.GetValue<string>("Auth0Domain", "DEFAULT_AUTH0_DOMAIN") ?? "";
        options.ClientId = builder.Configuration.GetValue<string>("Auth0ClientId", "DEFAULT_AUTH0_CLIENT_ID") ?? "";
        options.Scope = "openid profile email";
    });

builder.Services.AddHttpClient();

builder.Services.AddScoped<IPayPalAuthentication, PayPalAuthentication>();

builder.Services.AddBlazorBootstrap();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // Prevent circular dependencies
//builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.Configuration["db"]));
builder.Services.AddDbContextFactory<KakeysSharedLib.Data.PostgresContext>(options => options.UseNpgsql(builder.Configuration["db"]));

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
app.UseRouting();
app.UseAntiforgery();
app.UseSession();
app.UseAuthorization();

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

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(HomeLib).Assembly);

app.Run();

public partial class Program { };