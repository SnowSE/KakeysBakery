
using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public partial class ProductService : IProductService
{
    private readonly PostgresContext _context;
    private readonly ILogger<ProductService> _logger;
    [LoggerMessage(Level = LogLevel.Information, Message = "Getting All Products.")]
    static partial void GetAllProducts(ILogger logger, string description);

    public ProductService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
        Product? product = _context.Products.FirstOrDefault(b => b.Id == productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Product>> GetProductListAsync()
    {
        GetAllProducts(_logger, $"Inside getAllProducts now. Number of products is {_context.Products.Count()}");
        return await _context.Products.ToListAsync() ?? [];
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
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}