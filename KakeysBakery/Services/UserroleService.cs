using KakeysBakery.Data;

using KakeysBakeryClassLib.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class UserroleService : IUserroleService
{
    readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public UserroleService(KakeysBakeryClassLib.Data.PostgresContext context)
    {
        _context = context;
    }
    public async Task CreateUserroleAsync(Userrole userrole)
    {
        try
        {
            _context.Userroles.Add(userrole);
            await _context.SaveChangesAsync();
        }
        catch { }
    }

    public async Task DeleteUserroleAsync(int userroleID)
    {
        try
        {
            Userrole? userrole = await _context.Userroles.FirstOrDefaultAsync(a => a.Id == userroleID);
            if (userrole != null)
            {
                _context.Userroles.Remove(userrole);
                await _context.SaveChangesAsync();
            }
        }
        catch { }
    }

    public async Task<List<Userrole>> GetUserroleListAsync()
    {
        try
        {
            return await _context.Userroles.ToListAsync();
        }
        catch { return new List<Userrole>(); }
    }

    public async Task<Userrole?> GetUserroleAsync(int id)
    {
        return await _context.Userroles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateUserroleAsync(Userrole userrole)
    {
        try
        {
            _context.Userroles.Update(userrole);
            await _context.SaveChangesAsync();
        }
        catch { }
    }
}