
using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class PurchaseProductService : IPurchaseProductService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public PurchaseProductService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreatePurchaseProductAsync(PurchaseProduct purchaseProduct)
    {
        try
        {
            _context.PurchaseProducts.Add(purchaseProduct);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeletePurchaseProductAsync(int baseGoodId)
    {
        try
        {
            PurchaseProduct? purchaseProduct = _context.PurchaseProducts.FirstOrDefault(b => b.Id == baseGoodId);
            if (purchaseProduct != null)
            {
                _context.PurchaseProducts.Remove(purchaseProduct);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
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

    public async Task UpdatePurchaseProductAsync(PurchaseProduct purchaseProduct)
    {
        try
        {
            _context.PurchaseProducts.Update(purchaseProduct);
            await _context.SaveChangesAsync();
        }
        catch { }
    }
}