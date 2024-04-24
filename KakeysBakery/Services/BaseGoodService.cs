using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public partial class BaseGoodService : IBaseGoodService
{
    private readonly PostgresContext _context;
    private readonly ILogger<BaseGoodService> _logger;


    [LoggerMessage(Level = LogLevel.Information, Message = "Getting All BaseGoods.")]
    static partial void GetAllBaseGoods(ILogger logger, string description);
    public BaseGoodService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateBaseGoodAsync(Basegood basegood)
    {
        _context.Basegoods.Add(basegood);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBaseGoodAsync(int baseGoodId)
    {
        Basegood? basegood = _context.Basegoods.FirstOrDefault(b => b.Id == baseGoodId);
        if (basegood != null)
        {
            _context.Basegoods.Remove(basegood);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Basegood>> GetBaseGoodListAsync()
    {
        GetAllBaseGoods(_logger, $"Inside getAllBaseGoods now. Number of BaseGoods is {_context.Basegoods.Count()}");

        return await _context.Basegoods.ToListAsync() ?? [];
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
        _context.Basegoods.Update(basegood);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Basegood>> GetBasegoodsFromTypeAsync(int BasegoodTypeId)
    {
        return await _context.Basegoods
            .Where(i => i.Typeid == BasegoodTypeId)
            .Include(i => i.Flavor)
            .ToListAsync() ?? [];
    }
}