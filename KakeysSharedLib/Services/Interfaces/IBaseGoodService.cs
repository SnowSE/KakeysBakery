using KakeysSharedlib.Data;


namespace KakeysSharedlib.Services.Interfaces;

public interface IBaseGoodService
{
    public Task<List<Basegood>> GetBasegoodsFromTypeAsync(int BaseGoodTypeId);
    public Task<List<Basegood>> GetBaseGoodListAsync();
    public Task<Basegood?> GetBaseGoodAsync(int id);
    public Task<Basegood?> GetBaseGoodFromFlavorAsync(int id, int flavorid);


    public Task CreateBaseGoodAsync(Basegood basegood);
    public Task DeleteBaseGoodAsync(int basegoodId);
    public Task UpdateBaseGoodAsync(Basegood basegood);
}