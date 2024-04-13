using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodFlavorService : IBaseGoodFlavorService
{
    private readonly PostgresContext _context;
    public BaseGoodFlavorService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor)
    {
        _context.Basegoodflavors.Add(basegoodflavor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBaseGoodFlavorAsync(int baseGoodId)
    {
        Basegoodflavor? basegoodflavor = _context.Basegoodflavors.FirstOrDefault(b => b.Id == baseGoodId);
        if (basegoodflavor != null)
        {
            _context.Basegoodflavors.Remove(basegoodflavor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Basegoodflavor>> GetBaseGoodFlavorListAsync()
    {
        return await _context.Basegoodflavors.ToListAsync() ?? [];
    }

    public async Task<Basegoodflavor?> GetBaseGoodFlavorAsync(int id)
    {
        return await _context.Basegoodflavors
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Basegoodflavor> GetBaseGoodFlavorByBase(string flavor)
    {
        Basegoodflavor? basegoodflavor = await _context.Basegoodflavors
            .Where(b => b.Flavorname == flavor)
            .FirstOrDefaultAsync();
        return basegoodflavor!;
    }


    public async Task UpdateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor)
    {
        _context.Basegoodflavors.Update(basegoodflavor);
        await _context.SaveChangesAsync();
    }
}