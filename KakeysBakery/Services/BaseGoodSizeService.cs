using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodSizeService : IBasegoodSizeService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public BaseGoodSizeService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateBasegoodSizeAsync(BasegoodSize addonFlavor)
    {
        _context.BasegoodSizes.Add(addonFlavor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBasegoodSizeAsync(int addonFlavorId)
    {
        BasegoodSize? addonFlavor = _context.BasegoodSizes.FirstOrDefault(b => b.Id == addonFlavorId);
        if (addonFlavor != null)
        {
            _context.BasegoodSizes.Remove(addonFlavor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<BasegoodSize>> GetBasegoodSizeListAsync()
    {
        return await _context.BasegoodSizes.ToListAsync() ?? [];
    }

    public async Task<BasegoodSize?> GetBasegoodSizeAsync(int id)
    {
        return await _context.BasegoodSizes
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateBasegoodSizeAsync(BasegoodSize addonFlavor)
    {
        _context.BasegoodSizes.Update(addonFlavor);
        await _context.SaveChangesAsync();
    }

    public async Task<BasegoodSize?> GetBasegoodSizeByAsync(string flavor)
    {
        return await _context.BasegoodSizes
            .Where(f => f.Size == flavor)
            .FirstOrDefaultAsync();
    }
}