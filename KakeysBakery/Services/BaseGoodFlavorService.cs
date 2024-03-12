using KakeysBakery.Data;
using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodFlavorService : IBaseGoodFlavorService
{
    private PostgresContext _context;
    public BaseGoodFlavorService(PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor)
    {
        try
        {
            _context.Basegoodflavors.Add(basegoodflavor);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteBaseGoodFlavorAsync(int baseGoodId)
    {
        try
        {
            Basegoodflavor? basegoodflavor = _context.Basegoodflavors.FirstOrDefault(b => b.Id == baseGoodId);
            if (basegoodflavor != null)
            {
                _context.Basegoodflavors.Remove(basegoodflavor);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
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

    public Task UpdateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor)
    {
        try
        {
            _context.Basegoodflavors.Update(basegoodflavor);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}
