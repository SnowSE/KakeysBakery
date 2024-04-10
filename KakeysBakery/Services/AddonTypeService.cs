using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class AddonTypeService : IAddonTypeService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public AddonTypeService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateAddonTypeAsync(Addontype addontype)
    {
        try
        {
            _context.Addontypes.Add(addontype);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteAddonTypeAsync(int addonTypeId)
    {
        try
        {
            Addontype? addontype = _context.Addontypes.FirstOrDefault(b => b.Id == addonTypeId);
            if (addontype != null)
            {
                _context.Addontypes.Remove(addontype);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<Addontype>> GetAddonTypeListAsync()
    {
        try
        {
            return await _context.Addontypes.ToListAsync();
        }
        catch { return new List<Addontype>(); }
    }

    public async Task<Addontype?> GetAddonTypeAsync(int id)
    {
        return await _context.Addontypes
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public Task UpdateAddonTypeAsync(Addontype addontype)
    {
        try
        {
            _context.Addontypes.Update(addontype);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}