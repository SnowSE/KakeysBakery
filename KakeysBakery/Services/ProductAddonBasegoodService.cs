using BlazorBootstrap;

using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class ProductAddonBasegoodService : IProductAddonBasegoodService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public ProductAddonBasegoodService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood)
    {
        _context.ProductAddonBasegoods.Add(productAddonBasegood);

        await _context.SaveChangesAsync();
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

    public async Task<ProductAddonBasegood?> GetProductAddonBasegoodByProductIdAsync(int productId)
    {
        var pab = await _context.ProductAddonBasegoods
            .Where(b => b.Productid == productId)
            .FirstOrDefaultAsync();

        var test = await _context.ProductAddonBasegoods
            .ToListAsync();

        return pab;
    }

    public async Task<ProductAddonBasegood?> GetProductAddonBasegoodAsync(int flavorId, int typeId)
    {
        return await _context.ProductAddonBasegoods
            .Include(p => p.Basegood)
            .Where(p => p.Addon == null)
            .Where(p => p.Basegood!.Typeid == typeId)
            .Where(p => p.Basegood!.Flavorid == flavorId)
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