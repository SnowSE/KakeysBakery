using KakeysBakery.Data;

using KakeysBakeryClassLib.Data;
using KakeysBakeryClassLib.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class AddOnService : IAddonService
{
    readonly KakeysBakery.Data.PostgresContext _context;
    public AddOnService(KakeysBakery.Data.PostgresContext context)
    {
        _context = context;
    }
    public async Task CreateAddOnAsync(Addon addon)
    {
        try
        {
            _context.Addons.Add(addon);
            await _context.SaveChangesAsync();
        }
        catch { }
        await Task.CompletedTask;
    }

    public async Task DeleteAddOnAsync(int addonID)
    {
        try
        {
            Addon? addon = await _context.Addons.FirstOrDefaultAsync(a => a.Id == addonID);
            if (addon != null)
            {
                _context.Addons.Remove(addon);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
    }

    public async Task<List<Addon>> GetAddonListAsync()
    {
        try
        {
            return await _context.Addons.ToListAsync();
        }
        catch { return new List<Addon>(); }
    }

    public async Task<Addon?> GetAddonAsync(int id)
    {
        return await _context.Addons
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateAddOnAsync(Addon addon)
    {
        try
        {
            _context.Addons.Update(addon);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task<List<Addon>> GetAddonListFromType(int id)
    {
        try
        {
            return await _context.Addons.Where(t => t.Addontypeid == id).ToListAsync();
        }
        catch { return new List<Addon>(); }
    }
}