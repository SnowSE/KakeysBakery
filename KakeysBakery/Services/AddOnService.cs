using KakeysBakery.Data;

using KakeysSharedLib.Data;
using KakeysSharedLib.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class AddOnService : IAddonService
{
    readonly PostgresContext _context;
    public AddOnService(PostgresContext context)
    {
        _context = context;
    }
    public async Task CreateAddOnAsync(Addon addon)
    {
        _context.Addons.Add(addon);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAddOnAsync(int addonID)
    {
        Addon? addon = await _context.Addons.FirstOrDefaultAsync(a => a.Id == addonID);
        if (addon != null)
        {
            _context.Addons.Remove(addon);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Addon>> GetAddonListAsync()
    {
        return await _context.Addons.ToListAsync() ?? [];
    }

    public async Task<Addon?> GetAddonAsync(int id)
    {
        return await _context.Addons
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateAddOnAsync(Addon addon)
    {
        _context.Addons.Update(addon);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Addon>> GetAddonListFromType(int id)
    {
        return await _context.Addons.Where(t => t.Addontypeid == id).ToListAsync() ?? [];
    }
}