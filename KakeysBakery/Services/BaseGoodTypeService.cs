using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public partial class BaseGoodTypeService : IBaseGoodTypeService
{
    private readonly PostgresContext _context;
    private readonly ILogger<BaseGoodTypeService> _logger;
    [LoggerMessage(Level = LogLevel.Information, Message = "Getting All BaseGoodTypes.")]
    static partial void GetAllBaseGoodTypes(ILogger logger, string description);

    public BaseGoodTypeService(PostgresContext pc)
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
        GetAllBaseGoodTypes(_logger, $"Inside getAllBaseGoodTypes now. Number of BasegoodTypes is {_context.Basegoodtypes.Count()}");

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