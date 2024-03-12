using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IAddonTypeService
{
    public Task<List<Addontype>> GetAddonTypeListAsync();
    public Task<Addontype?> GetAddonTypeAsync(int id);
    public Task CreateAddonTypeAsync(Addontype addontype);
    public Task DeleteAddonTypeAsync(int addontypeId);
    public Task UpdateAddonTypeAsync(Addontype addontype);
}
