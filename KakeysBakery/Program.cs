using System.Text.Json.Serialization;

using Auth0.AspNetCore.Authentication;

using KakeysBakery.Components;
using KakeysBakery.Components.AuthenticationStateSyncer;
using KakeysBakery.Components.OAuth;
using KakeysBakery.Data;
using KakeysBakery.Services;

using KakeysSharedLib.Pages;
using KakeysSharedLib.Services.Implementations;
using KakeysSharedLib.Telemetry;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


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
//builder.Services.AddScoped<IPayPalAuthentication, PayPalAuthentication>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration.GetValue<string>("Auth0Domain", "DEFAULT_AUTH0_DOMAIN") ?? "";
        options.ClientId = builder.Configuration.GetValue<string>("Auth0ClientId", "DEFAULT_AUTH0_CLIENT_ID") ?? "";
        options.Scope = "openid profile email";
    });

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = $"https://{builder.Configuration.GetValue<string>("Auth0Domain", "DEFAULT_AUTH0_DOMAIN") ?? ""}/";
//    options.TokenValidationParameters =
//      new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//      {
//          ValidAudience = "kakeys_bakery",
//          ValidIssuer = $"{builder.Configuration.GetValue<string>("Auth0Domain", "DEFAULT_AUTH0_DOMAIN") ?? ""}",
//          ValidateLifetime = true,
//      };
//});

builder.Services.AddScoped(o =>
{
    var client = new HttpClient
    {
        //BaseAddress = new Uri("https://localhost:7196")
        BaseAddress = new Uri(builder.Configuration["BaseUri"] ?? "https://kakeysbakery20240319120850.azurewebsites.net/")
    };
    return client;
});

//for feature flag requirement
FeatureFlagService.SetVariable(builder.Configuration.GetValue<string>("FeatureFlag") == "true");
FeatureFlagService.SetVariable2(builder.Configuration.GetValue<string>("IsOnMaui") == "true");

builder.Services.AddBlazorBootstrap();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // Prevent circular dependencies
//builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.Configuration["db"]));
builder.Services.AddDbContextFactory<KakeysSharedLib.Data.PostgresContext>(options => options.UseNpgsql(builder.Configuration["db"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
              { securitySchema, new[] { "Bearer" } }
          });
});



const string serviceName = "otelService";
const string otelEndpoint = "http://otel-collector-service:4317/";

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault().AddService(serviceName))
        .AddOtlpExporter(o =>
            o.Endpoint = new Uri(otelEndpoint))
        .AddConsoleExporter();
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("FirstTrace"))
    .WithTracing(tracing => tracing
        //.AddSource(serviceName)
        //.AddSource(Traces.Name)
        //.AddSource(Traces.Name2)
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(o =>
            o.Endpoint = new Uri(otelEndpoint)))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddMeter(Metrics.Name)
        // .AddConsoleExporter()
        .AddPrometheusExporter()
        .AddOtlpExporter(o =>
            o.Endpoint = new Uri(otelEndpoint)));


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
app.UseStaticFiles();
app.UseAntiforgery();
app.UseRouting();
app.UseAntiforgery();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); // Needs to be below Auth

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

//app.MapControllerRoute(
//name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapPrometheusScrapingEndpoint();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(HomeLib).Assembly);
app.Run();

public partial class Program { };