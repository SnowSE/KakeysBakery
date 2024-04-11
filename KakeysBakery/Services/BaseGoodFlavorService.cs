using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodFlavorService : IBaseGoodFlavorService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public BaseGoodFlavorService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor)
    {
        try
        {
            _context.Basegoodflavors.Add(basegoodflavor);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteBaseGoodFlavorAsync(int baseGoodId)
    {
        try
        {
            Basegoodflavor? basegoodflavor = _context.Basegoodflavors.FirstOrDefault(b => b.Id == baseGoodId);
            if (basegoodflavor != null)
            {
                _context.Basegoodflavors.Remove(basegoodflavor);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
    }

    public async Task<List<Basegoodflavor>> GetBaseGoodFlavorListAsync()
    {
        try
        {
            return await _context.Basegoodflavors.ToListAsync();
        }
        catch { return new List<Basegoodflavor>(); }
    }

    public async Task<Basegoodflavor?> GetBaseGoodFlavorAsync(int id)
    {
        return await _context.Basegoodflavors
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task<Basegoodflavor> GetBaseGoodFlavorByBase(string flavor)
    {
        Basegoodflavor? basegoodflavor = await _context.Basegoodflavors.Where(b => b.Flavorname == flavor).FirstOrDefaultAsync();
        return basegoodflavor!;
    }


    public async Task UpdateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor)
    {
        try
        {
            _context.Basegoodflavors.Update(basegoodflavor);
            await _context.SaveChangesAsync();
        }
        catch { }
    }
}