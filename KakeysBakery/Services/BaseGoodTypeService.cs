using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodTypeService : IBaseGoodTypeService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public BaseGoodTypeService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateBaseGoodTypeAsync(Basegoodtype baseGoodType)
    {
        _context.Basegoodtypes.Add(baseGoodType);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBaseGoodTypeAsync(int baseGoodId)
    {
        Basegoodtype? baseGoodType = _context.Basegoodtypes.FirstOrDefault(b => b.Id == baseGoodId);
        if (baseGoodType != null)
        {
            _context.Basegoodtypes.Remove(baseGoodType);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Basegoodtype>> GetBaseGoodTypeListAsync()
    {
        return await _context.Basegoodtypes.ToListAsync() ?? [];
    }

    public async Task<Basegoodtype?> GetBaseGoodTypeAsync(int id)
    {
        return await _context.Basegoodtypes
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateBaseGoodTypeAsync(Basegoodtype baseGoodType)
    {
        _context.Basegoodtypes.Update(baseGoodType);
        await _context.SaveChangesAsync();
    }

    public async Task<Basegoodtype?> GetBaseGoodTypeByBase(string basegood)
    {
        return await _context.Basegoodtypes.Where(t => t.Basegood == basegood).FirstOrDefaultAsync();
    }
}