using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodSizeService : IBasegoodSizeService
{
    private readonly KakeysBakery.Data.PostgresContext _context;
    public BaseGoodSizeService(KakeysBakery.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateBasegoodSizeAsync(BasegoodSize addonFlavor)
    {
        try
        {
            addonFlavor.Id = _context.BasegoodSizes.Count() + 1;
            _context.BasegoodSizes.Add(addonFlavor);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteBasegoodSizeAsync(int addonFlavorId)
    {
        try
        {
            BasegoodSize? addonFlavor = _context.BasegoodSizes.FirstOrDefault(b => b.Id == addonFlavorId);
            if (addonFlavor != null)
            {
                _context.BasegoodSizes.Remove(addonFlavor);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<BasegoodSize>> GetBasegoodSizeListAsync()
    {
        try
        {
            return await _context.BasegoodSizes.ToListAsync();
        }
        catch { return new List<BasegoodSize>(); }
    }

    public async Task<BasegoodSize?> GetBasegoodSizeAsync(int id)
    {
        return await _context.BasegoodSizes
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public Task UpdateBasegoodSizeAsync(BasegoodSize addonFlavor)
    {
        try
        {
            _context.BasegoodSizes.Update(addonFlavor);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<BasegoodSize?> GetBasegoodSizeByAsync(string flavor)
    {
        return await _context.BasegoodSizes.Where(f => f.Size == flavor).FirstOrDefaultAsync();
    }
}