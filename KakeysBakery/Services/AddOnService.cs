using KakeysBakery.Data;
using KakeysBakeryClassLib.Data;
using KakeysBakeryClassLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class AddOnService : IAddonService
{
    PostgresContext _context;
    public AddOnService(PostgresContext context)
    {
        _context = context;
    }
    public Task CreateAddOnAsync(Addon addon)
    {
        try
        {
            _context.Addons.Add(addon);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task DeleteAddOnAsync(int addonID)
    {
        try
        {
            Addon addon = await _context.Addons.FirstOrDefaultAsync(a => a.Id == addonID);
            if (addon != null)
            {
                _context.Addons.Remove(addon);
                _context.SaveChanges();
            }
        }
        catch {}
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

    public async Task<Addon?> GetAddonAsync(string name)
    {
        return await _context.Addons
                .Where(a => a.Addontypename == name)
                .FirstOrDefaultAsync();
    }

    public Task UpdateAddOnAsync(Addon addon)
    {
        try
        {
            _context.Addons.Update(addon);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}
