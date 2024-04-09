using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IPurchaseService
{
    public Task<List<Purchase>> GetPurchaseListAsync();
    public Task<Purchase?> GetPurchaseAsync(int purchaseId);
    public Task CreatePurchaseAsync(Purchase purchase);
    public Task DeletePurchaseAsync(int purchaseId);
    public Task UpdatePurchaseAsync(Purchase purchase);
}