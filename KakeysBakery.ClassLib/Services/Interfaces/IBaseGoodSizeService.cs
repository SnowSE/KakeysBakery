using KakeysBakeryClassLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysBakeryClassLib.Services.Interfaces
{
	public  interface IBasegoodSizeService
	{
			public Task<List<BasegoodSize>> GetBasegoodSizeListAsync();
			public Task<BasegoodSize?> GetBasegoodSizeAsync(int id);
			public Task CreateBasegoodSizeAsync(BasegoodSize addon);
			public Task DeleteBasegoodSizeAsync(int addonId);
			public Task UpdateBasegoodSizeAsync(BasegoodSize addon);
			public Task<BasegoodSize?> GetBasegoodSizeByAsync(string flavor);
	}
}
