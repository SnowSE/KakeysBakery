using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class AddonFlavorService : IAddonFlavorService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public AddonFlavorService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateAddonFlavorAsync(Addonflavor addonFlavor)
    {
        addonFlavor.Id = addonFlavor.Id;
        await _context.Addonflavors.AddAsync(addonFlavor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAddonFlavorAsync(int addonFlavorId)
    {
        Addonflavor? addonFlavor = _context.Addonflavors.FirstOrDefault(b => b.Id == addonFlavorId);
        if (addonFlavor != null)
        {
            _context.Addonflavors.Remove(addonFlavor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Addonflavor>> GetAddonFlavorListAsync()
    {
        return await _context.Addonflavors.ToListAsync() ?? new();
    }

    public async Task<Addonflavor?> GetAddonFlavorAsync(int id)
    {
        return await _context.Addonflavors
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateAddonFlavorAsync(Addonflavor addonFlavor)
    {
        _context.Addonflavors.Update(addonFlavor);
        await _context.SaveChangesAsync();
    }

    public async Task<Addonflavor?> GetAddonFlavorByFlavorAsync(string flavor)
    {
        return await _context.Addonflavors
            .Where(f => f.Flavor == flavor)
            .FirstOrDefaultAsync();
    }
}