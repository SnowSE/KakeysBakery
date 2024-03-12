using KakeysBakery.Data;
using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class ProductAddonBasegoodService : IProductAddonBasegoodService
{
    private PostgresContext _context;
    public ProductAddonBasegoodService(PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood)
    {
        try
        {
            _context.ProductAddonBasegoods.Add(productAddonBasegood);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteProductAddonBasegoodAsync(int productAddonBasegoodId)
    {
        try
        {
            ProductAddonBasegood? productAddonBasegood = _context.ProductAddonBasegoods.FirstOrDefault(b => b.Id == productAddonBasegoodId);
            if (productAddonBasegood != null)
            {
                _context.ProductAddonBasegoods.Remove(productAddonBasegood);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<ProductAddonBasegood>> GetProductAddonBasegoodListAsync()
    {
        try
        {
            return await _context.ProductAddonBasegoods.ToListAsync();
        }
        catch { return new List<ProductAddonBasegood>(); }
    }

    public async Task<ProductAddonBasegood?> GetProductAddonBasegoodAsync(int id)
    {
        return await _context.ProductAddonBasegoods
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public Task UpdateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood)
    {
        try
        {
            _context.ProductAddonBasegoods.Update(productAddonBasegood);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}