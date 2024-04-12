using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodService : IBaseGoodService
{
    private readonly PostgresContext _context;
    public BaseGoodService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateBaseGoodAsync(Basegood basegood)
    {
        try
        {
            _context.Basegoods.Add(basegood);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteBaseGoodAsync(int baseGoodId)
    {
        try
        {
            Basegood? basegood = _context.Basegoods.FirstOrDefault(b => b.Id == baseGoodId);
            if (basegood != null)
            {
                _context.Basegoods.Remove(basegood);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
    }

    public async Task<List<Basegood>> GetBaseGoodListAsync()
    {
        try
        {
            return await _context.Basegoods.ToListAsync();
        }
        catch { return new List<Basegood>(); }
    }

    public async Task<Basegood?> GetBaseGoodAsync(int id)
    {
        return await _context.Basegoods
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task<Basegood?> GetBaseGoodFromFlavorAsync(int typeId, int flavorid)
    {
        return await _context.Basegoods
                .Where(b => b.Typeid == typeId)
                .Where(b => b.Flavorid == flavorid)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateBaseGoodAsync(Basegood basegood)
    {
        try
        {
            _context.Basegoods.Update(basegood);
            await _context.SaveChangesAsync();
        }
        catch { }
    }


    public async Task<List<Basegood>> GetBasegoodsFromTypeAsync(int BasegoodTypeId)
    {
        try
        {
            return await _context.Basegoods
                .Where(i => i.Typeid == BasegoodTypeId)
                .Include(i => i.Flavor)
                .ToListAsync();
        }
        catch { return new List<Basegood>(); }
    }
}