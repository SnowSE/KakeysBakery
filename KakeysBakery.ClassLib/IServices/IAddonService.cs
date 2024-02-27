using KakeysBakeryClassLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysBakeryClassLib.IServices;

public interface IAddonService
{
    public Task<List<Addon>> GetAddonListAsync();

    public Task CreateAddOnAsync(Addon addon);

    public Task DeleteAddOnAsync(Addon addon);

    public Task UpdateAddOnAsync(Addon addon);
}
