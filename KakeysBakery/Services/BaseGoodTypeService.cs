using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodTypeService : IBaseGoodTypeService
{
    private readonly PostgresContext _context;
    public BaseGoodTypeService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateBaseGoodTypeAsync(Basegoodtype baseGoodType)
    {
        try
        {
            _context.Basegoodtypes.Add(baseGoodType);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteBaseGoodTypeAsync(int baseGoodId)
    {
        try
        {
            Basegoodtype? baseGoodType = _context.Basegoodtypes.FirstOrDefault(b => b.Id == baseGoodId);
            if (baseGoodType != null)
            {
                _context.Basegoodtypes.Remove(baseGoodType);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
    }

    public async Task<List<Basegoodtype>> GetBaseGoodTypeListAsync()
    {
        try
        {
            return await _context.Basegoodtypes.ToListAsync();
        }
        catch { return new List<Basegoodtype>(); }
    }

    public async Task<Basegoodtype?> GetBaseGoodTypeAsync(int id)
    {
        return await _context.Basegoodtypes
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateBaseGoodTypeAsync(Basegoodtype baseGoodType)
    {
        try
        {
            _context.Basegoodtypes.Update(baseGoodType);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task<Basegoodtype?> GetBaseGoodTypeByBase(string basegood)
    {
        return await _context.Basegoodtypes.Where(t => t.Basegood == basegood).FirstOrDefaultAsync();
    }
}