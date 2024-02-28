using KakeysBakery.Data;
using KakeysBakeryClassLib.Data;
using KakeysBakeryClassLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class PurchaseService : IPurchaseService
{
    PostgresContext _context;
    public PurchaseService(PostgresContext context)
    {
        _context = context;
    }
    public Task CreatePurchaseAsync(Purchase purchase)
    {
        try
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task DeletePurchaseAsync(int purchaseID)
    {
        try
        {
            Purchase? purchase = await _context.Purchases.FirstOrDefaultAsync(a => a.Id == purchaseID);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
                _context.SaveChanges();
            }
        }
        catch { }
    }

    public async Task<List<Purchase>> GetPurchaseListAsync()
    {
        try
        {
            return await _context.Purchases.ToListAsync();
        }
        catch { return new List<Purchase>(); }
    }

    public async Task<Purchase?> GetPurchaseAsync(int id)
    {
        return await _context.Purchases
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
    }

    public Task UpdatePurchaseAsync(Purchase purchase)
    {
        try
        {
            _context.Purchases.Update(purchase);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}
