
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
        _context.PurchaseProducts.Add(purchaseProduct);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePurchaseProductAsync(int baseGoodId)
    {
        PurchaseProduct? purchaseProduct = _context.PurchaseProducts.FirstOrDefault(b => b.Id == baseGoodId);
        if (purchaseProduct != null)
        {
            _context.PurchaseProducts.Remove(purchaseProduct);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<PurchaseProduct>> GetPurchaseProductListAsync()
    {
        return await _context.PurchaseProducts.ToListAsync() ?? [];
    }

    public async Task<PurchaseProduct?> GetPurchaseProductAsync(int id)
    {
        return await _context.PurchaseProducts
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdatePurchaseProductAsync(PurchaseProduct purchaseProduct)
    {
        _context.PurchaseProducts.Update(purchaseProduct);
        await _context.SaveChangesAsync();
    }
}