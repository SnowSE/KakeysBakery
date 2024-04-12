using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IPurchaseProductService
{
    public Task<List<PurchaseProduct>> GetPurchaseProductListAsync();
    public Task<PurchaseProduct?> GetPurchaseProductAsync(int purchaseProductId);
    public Task CreatePurchaseProductAsync(PurchaseProduct purchaseProduct);
    public Task DeletePurchaseProductAsync(int purchaseProductId);
    public Task UpdatePurchaseProductAsync(PurchaseProduct purchaseProduct);
}