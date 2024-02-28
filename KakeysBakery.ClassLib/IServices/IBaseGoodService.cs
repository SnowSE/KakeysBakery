﻿using KakeysBakeryClassLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysBakeryClassLib.IServices;

public interface IBaseGoodService
{
    public Task<List<Basegood>> GetBaseGoodListAsync();

    public Task CreateBaseGoodAsync(Basegood basegood);

    public Task DeleteBaseGoodAsync(int basegoodId);

    public Task UpdateBaseGoodAsync(Basegood basegood);
}
