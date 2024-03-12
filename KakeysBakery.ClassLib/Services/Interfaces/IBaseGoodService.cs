using KakeysBakeryClassLib.Data;


namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IBaseGoodService
{
    public Task<List<Basegood>> GetBaseGoodListAsync();
    public Task<Basegood?> GetBaseGoodAsync(int id);
    public Task CreateBaseGoodAsync(Basegood basegood);
    public Task DeleteBaseGoodAsync(int basegoodId);
    public Task UpdateBaseGoodAsync(Basegood basegood);
}
