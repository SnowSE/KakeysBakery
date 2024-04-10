﻿using KakeysBakery.Data;

using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public class CartService : ICartService
{
    private readonly KakeysBakeryClassLib.Data.PostgresContext _context;
    public CartService(KakeysBakeryClassLib.Data.PostgresContext pc)
    {
        _context = pc;
    }
    public Task CreateCartAsync(Cart cart)
    {
        try
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public Task DeleteCartAsync(int cartId)
    {
        try
        {
            Cart? cart = _context.Carts.FirstOrDefault(b => b.Id == cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<List<Cart>> GetCartListAsync()
    {
        try
        {
            return await _context.Carts.ToListAsync();
        }
        catch { return new List<Cart>(); }
    }

    public async Task<Cart?> GetCartAsync(int id)
    {
        return await _context.Carts
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public Task UpdateCartAsync(Cart cart)
    {
        try
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }
        catch { }
        return Task.CompletedTask;
    }

    public async Task<int> AddToCustomersCart(int CustomerId, int BasegoodId)
    {
        //TODO: fix this

        await Task.CompletedTask;
        return -1;
    }
}