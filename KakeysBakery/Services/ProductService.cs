﻿
using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class ProductService : IProductService
{
    private readonly PostgresContext _context;
    public ProductService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateProductAsync(Product product)
    {
        try
        {
            _context.Products.Add(product);
                _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteProductAsync(int productId)
    {
        try
        {
            Product? product = _context.Products.FirstOrDefault(b => b.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
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

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        catch { }
    }
}