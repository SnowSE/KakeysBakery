
using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class PurchaseProductService : IPurchaseProductService
{
    private readonly KakeysBakery.Data.PostgresContext _context;
    public PurchaseProductService(KakeysBakery.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreatePurchaseProductAsync(PurchaseProduct purchaseProduct)
    {
        try
        {
            _context.PurchaseProducts.Add(purchaseProduct);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeletePurchaseProductAsync(int baseGoodId)
    {
        try
        {
            PurchaseProduct? purchaseProduct = _context.PurchaseProducts.FirstOrDefault(b => b.Id == baseGoodId);
            if (purchaseProduct != null)
            {
                _context.PurchaseProducts.Remove(purchaseProduct);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<PurchaseProduct>> GetPurchaseProductListAsync()
    {
        try
        {
            return await _context.PurchaseProducts.ToListAsync();
        }
        catch { return new List<PurchaseProduct>(); }
    }

    public async Task<PurchaseProduct?> GetPurchaseProductAsync(int id)
    {
        return await _context.PurchaseProducts
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public Task UpdatePurchaseProductAsync(PurchaseProduct purchaseProduct)
    {
        try
        {
            _context.PurchaseProducts.Update(purchaseProduct);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}