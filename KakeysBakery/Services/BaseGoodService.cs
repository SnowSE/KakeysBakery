using KakeysBakery.Data;
using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class BaseGoodService : IBaseGoodService
{
    private PostgresContext _context;
    public BaseGoodService(PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateBaseGoodAsync(Basegood basegood)
    {
        try
        {
            _context.Basegoods.Add(basegood);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteBaseGoodAsync(int baseGoodId)
    {
        try
        {
            Basegood? basegood = _context.Basegoods.FirstOrDefault(b => b.Id == baseGoodId);
            if (basegood != null)
            {
                _context.Basegoods.Remove(basegood);
                _context.SaveChanges();
            }
        } catch {  } 
        return Task.CompletedTask;
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

    public async Task<Basegood?> GetBaseGoodAsync(string name)
    {
        return await _context.Basegoods
                .Where(b => b.Basegoodname == name)
                .FirstOrDefaultAsync();
    }

    public Task UpdateBaseGoodAsync(Basegood basegood)
    {
        try
        {
            _context.Basegoods.Update(basegood);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }
}
