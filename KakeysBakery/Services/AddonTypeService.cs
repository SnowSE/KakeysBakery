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
    public async Task CreateAddonTypeAsync(Addontype addontype)
    {
        try
        {
            _context.Addontypes.Add(addontype);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteAddonTypeAsync(int addonTypeId)
    {
        try
        {
            Addontype? addontype = _context.Addontypes.FirstOrDefault(b => b.Id == addonTypeId);
            if (addontype != null)
            {
                _context.Addontypes.Remove(addontype);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
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

    public async Task UpdateAddonTypeAsync(Addontype addontype)
    {
        try
        {
            _context.Addontypes.Update(addontype);
            await _context.SaveChangesAsync();
        }
        catch { }
    }
}