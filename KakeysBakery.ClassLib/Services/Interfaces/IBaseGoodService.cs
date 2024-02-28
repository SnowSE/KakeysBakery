using KakeysBakeryClassLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IBaseGoodService
{
    public Task<List<Basegood>> GetBaseGoodListAsync();
    public Task<Basegood?> GetBaseGoodAsync(int id);
    public Task<Basegood?> GetBaseGoodAsync(string name);
    public Task CreateBaseGoodAsync(Basegood basegood);
    public Task DeleteBaseGoodAsync(int basegoodId);
    public Task UpdateBaseGoodAsync(Basegood basegood);
}
