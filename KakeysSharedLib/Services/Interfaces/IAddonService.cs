﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IAddonService
{
    public Task<List<Addon>> GetAddonListAsync();
    public Task<List<Addon>> GetAddonListFromType(int id);

    public Task<Addon?> GetAddonAsync(int id);
    public Task CreateAddOnAsync(Addon addon);
    public Task DeleteAddOnAsync(int addonId);
    public Task UpdateAddOnAsync(Addon addon);
}