using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IProductAddonBasegoodService
{
    public Task<List<ProductAddonBasegood>> GetProductAddonBasegoodListAsync();
    public Task<ProductAddonBasegood?> GetProductAddonBasegoodAsync(int productAddonBasegoodId);
    public Task<ProductAddonBasegood?> GetProductAddonBasegoodAsync(int selectedTypeId, int typeId);
    public Task<ProductAddonBasegood?> GetProductAddonBasegoodByProductIdAsync(int productId);
    public Task CreateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood);
    public Task DeleteProductAddonBasegoodAsync(int productAddonBasegoodId);
    public Task UpdateProductAddonBasegoodAsync(ProductAddonBasegood productAddonBasegood);
}