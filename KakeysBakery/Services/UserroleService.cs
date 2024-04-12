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
        _context.Userroles.Add(userrole);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserroleAsync(int userroleID)
    {
        Userrole? userrole = await _context.Userroles.FirstOrDefaultAsync(a => a.Id == userroleID);
        if (userrole != null)
        {
            _context.Userroles.Remove(userrole);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Userrole>> GetUserroleListAsync()
    {
        return await _context.Userroles.ToListAsync() ?? [];
    }

    public async Task<Userrole?> GetUserroleAsync(int id)
    {
        return await _context.Userroles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task UpdateUserroleAsync(Userrole userrole)
    {
        _context.Userroles.Update(userrole);
        await _context.SaveChangesAsync();
    }
}