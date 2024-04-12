using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class ProductAddonBasegoodService : IProductAddonBasegoodService
{
    private readonly PostgresContext _context;
    public ProductAddonBasegoodService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood)
    {
        try
        {
            _context.ProductAddonBasegoods.Add(productAddonBasegood);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteProductAddonBasegoodAsync(int productAddonBasegoodId)
    {
        try
        {
            ProductAddonBasegood? productAddonBasegood = _context.ProductAddonBasegoods.FirstOrDefault(b => b.Id == productAddonBasegoodId);
            if (productAddonBasegood != null)
            {
                _context.ProductAddonBasegoods.Remove(productAddonBasegood);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
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

    public async Task UpdateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood)
    {
        try
        {
            _context.ProductAddonBasegoods.Update(productAddonBasegood);
            await _context.SaveChangesAsync();
        }
        catch { }
    }
}