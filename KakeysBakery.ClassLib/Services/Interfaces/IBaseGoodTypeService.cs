using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IBaseGoodTypeService
{
    public Task<List<Basegoodtype>> GetBaseGoodTypeListAsync();
    public Task<Basegoodtype?> GetBaseGoodTypeAsync(int id);
    public Task<Basegoodtype?> GetBaseGoodTypeByBase(string basegood);
    public Task CreateBaseGoodTypeAsync(Basegoodtype basegoodtype);
    public Task DeleteBaseGoodTypeAsync(int basegoodtypeId);
    public Task UpdateBaseGoodTypeAsync(Basegoodtype basegoodtype);
}
