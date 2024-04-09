using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IAddonFlavorService
{
    public Task<List<Addonflavor>> GetAddonFlavorListAsync();
    public Task<Addonflavor?> GetAddonFlavorAsync(int id);
    public Task CreateAddonFlavorAsync(Addonflavor addonFlavor);
    public Task DeleteAddonFlavorAsync(int addonFlavorId);
    public Task UpdateAddonFlavorAsync(Addonflavor addonFlavor);
    public Task<Addonflavor?> GetAddonFlavorByFlavorAsync(string flavor);
}