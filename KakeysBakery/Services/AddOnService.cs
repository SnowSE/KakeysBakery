using KakeysBakery.Data;
using KakeysBakeryClassLib.Data;
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

    public Task DeleteAddOnAsync(Addon addon)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Addon>> GetAddonListAsync()
    {
        try
        {
           return await _context.Addons.ToListAsync();
        }
        catch { return new List<Addon>(); }
    }

    public Task UpdateAddOnAsync(Addon addon)
    {
        throw new NotImplementedException();
    }
}
