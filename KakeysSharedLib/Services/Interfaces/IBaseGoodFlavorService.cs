﻿using KakeysSharedLib.Data;

namespace KakeysSharedLib.Services.Interfaces;

public interface IBaseGoodFlavorService
{
    public Task<List<Basegoodflavor>> GetBaseGoodFlavorListAsync();
    public Task<Basegoodflavor?> GetBaseGoodFlavorAsync(int id);
    public Task CreateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor);
    public Task DeleteBaseGoodFlavorAsync(int basegoodflavorId);
    public Task UpdateBaseGoodFlavorAsync(Basegoodflavor basegoodflavor);

    public Task<Basegoodflavor> GetBaseGoodFlavorByBase(string flavor);
}