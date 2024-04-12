using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysSharedLib.Data;

namespace KakeysSharedLib.Services.Interfaces;

public interface IProductService
{
    public Task<List<Product>> GetProductListAsync();
    public Task<Product?> GetProductAsync(int productId);
    public Task<Product?> GetProductAsync(string productname);
    public Task CreateProductAsync(Product product);
    public Task DeleteProductAsync(int productId);
    public Task UpdateProductAsync(Product product);
}