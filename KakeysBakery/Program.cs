using KakeysBakery.Components;
using KakeysBakery.Data;
using KakeysBakery.Services;
using KakeysBakeryClassLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();

public partial class Program { };