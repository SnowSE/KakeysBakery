
using KakeysBakery.Data;
using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class ProductService : IProductService
{
    private PostgresContext _context;
    public ProductService(PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateProductAsync(Product product)
    {
        try
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteProductAsync(int baseGoodId)
    {
        try
        {
            Product? product = _context.Products.FirstOrDefault(b => b.Id == baseGoodId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<Product>> GetProductListAsync()
    {
        try
        {
            return await _context.Products.ToListAsync();
        }
        catch { return new List<Product>(); }
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _context.Products
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task<Product?> GetProductAsync(string name)
    {
        return await _context.Products
                .Where(b => b.Productname == name)
                .FirstOrDefaultAsync();
    }

    public Task UpdateProductAsync(Product product)
    {
        try
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}
