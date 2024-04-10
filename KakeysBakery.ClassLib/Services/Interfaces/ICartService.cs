using KakeysBakeryClassLib.Data;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface ICartService
{
    public Task<List<Cart>> GetCartListAsync();
    public Task<Cart?> GetCartAsync(int id);
    public Task CreateCartAsync(Cart cart);
    public Task DeleteCartAsync(int cartId);
    public Task UpdateCartAsync(Cart cart);
    public Task<int> AddToCustomersCart(int CustomerId, int BasegoodId);
}