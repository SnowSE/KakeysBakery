using KakeysBakery.Data;

using KakeysSharedLib.Data;
using KakeysSharedLib.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class PurchaseService : IPurchaseService
{
    readonly PostgresContext _context;
    public PurchaseService( PostgresContext context)
    {
        _context = context;
    }
    public async Task CreatePurchaseAsync(Purchase purchase)
    {
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePurchaseAsync(int purchaseID)
    {
        Purchase? purchase = await _context.Purchases.FirstOrDefaultAsync(a => a.Id == purchaseID);
        if (purchase != null)
        {
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Purchase>> GetPurchaseListAsync()
    {
        return await _context.Purchases.ToListAsync() ?? [];
    }

    public async Task<Purchase?> GetPurchaseAsync(int id)
    {
        return await _context.Purchases
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdatePurchaseAsync(Purchase purchase)
    {
        _context.Purchases.Update(purchase);
        await _context.SaveChangesAsync();
    }
}