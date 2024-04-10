using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class AddonFlavorService : IAddonFlavorService
{
    private readonly KakeysBakery.Data.PostgresContext _context;
    public AddonFlavorService(KakeysBakery.Data.PostgresContext pc)
    {   
        _context = pc;
    }
    public Task CreateAddonFlavorAsync(Addonflavor addonFlavor)
    {
        try
        {
            addonFlavor.Id = addonFlavor.Id;
            _context.Addonflavors.Add(addonFlavor);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteAddonFlavorAsync(int addonFlavorId)
    {
        try
        {
            Addonflavor? addonFlavor = _context.Addonflavors.FirstOrDefault(b => b.Id == addonFlavorId);
            if (addonFlavor != null)
            {
                _context.Addonflavors.Remove(addonFlavor);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<Addonflavor>> GetAddonFlavorListAsync()
    {
        try
        {
            return await _context.Addonflavors.ToListAsync();
        }
        catch { return new List<Addonflavor>(); }
    }

    public async Task<Addonflavor?> GetAddonFlavorAsync(int id)
    {
        return await _context.Addonflavors
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public Task UpdateAddonFlavorAsync(Addonflavor addonFlavor)
    {
        try
        {
            _context.Addonflavors.Update(addonFlavor);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<Addonflavor?> GetAddonFlavorByFlavorAsync(string flavor)
    {
        return await _context.Addonflavors.Where(f => f.Flavor == flavor).FirstOrDefaultAsync();
    }
}