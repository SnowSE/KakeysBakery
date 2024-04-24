using KakeysBakery.Data;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace KakeysBakery.Services;

public partial class CartService : ICartService
{
    private readonly PostgresContext _context;


    public CartService(PostgresContext pc)
    {
        _context = pc;
    }
    public async Task CreateCartAsync(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCartAsync(int cartId)
    {
        Cart? cart = _context.Carts.FirstOrDefault(b => b.Id == cartId);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Cart>> GetCartListAsync()
    {

        return await _context.Carts.ToListAsync() ?? [];
    }

    public async Task<Cart?> GetCartAsync(int id)
    {
        return await _context.Carts
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
    }

    public async Task<Cart?> GetCartFromEmailAsync(string email)
    {
        return await _context.Carts
            .Include(b => b.Customer)
            .Where(b => b.Customer!.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddToCustomersCart(int CustomerId, int BasegoodId)
    {
        //TODO: fix this
        int? cartId = null;
        try
        {
            Basegood? good = _context.Basegoods.Where(b => b.Id == BasegoodId).FirstOrDefault();
            Customer? cust = _context.Customers.Where(c => c.Id == CustomerId).FirstOrDefault();
            if (good != null && cust != null)
            {
                var prodAddOnBaseGood = _context.ProductAddonBasegoods.Where(p => p.Basegoodid == BasegoodId)
                    .Where(p => p.Basegood!.Typeid == good.Typeid)
                    .Where(p => p.Addonid == null)
                    .ToList();
                int prodId;
                if (prodAddOnBaseGood is null)
                {
                    //Create product for basegood
                    Product newprod = new();
                    _context.Products.Add(newprod);
                    await _context.SaveChangesAsync();
                    prodId = newprod.Id;

                    ProductAddonBasegood newaddonbase = new()
                    {
                        Basegoodid = BasegoodId,
                        Productid = prodId
                    };
                }
                else if (prodAddOnBaseGood.Count >= 1)
                {
                    //Get prod Id
                    var prod = _context.Products.Where(p => p.Id == prodAddOnBaseGood[0].Id).FirstOrDefault();
                    if (prod == null) { throw new NullReferenceException("Product Id is Null!"); };
                    prodId = prod.Id;
                }
                else
                {
                    throw new Exception("something went wrong trying to get a product id");
                }

                //add to cart
                Cart cart = new()
                {
                    Customerid = CustomerId,
                    Productid = prodId
                };
            }
            else throw new Exception("either the customer or basegood couldn't be found");
        }
        catch { throw; }
        return cartId ?? -1;
    }

    //return the purchase Id
    public async Task<int> PerformCheckoutLogicAsync(int customerId)
    {
        List<Cart> carts = new();
        //get all cart items connected to current customer
        carts = _context.Carts.Where(c => c.Customerid == customerId)
            .ToList();

        // Get the total price in the cart
        // (stolen from cart controller)
        decimal price = 0m;
        foreach (Cart c in carts)
        {
            decimal total = 0.0m;
            var pabs = await _context.ProductAddonBasegoods
                .Include(p => p.Product)
                .Include(p => p.Basegood)
                .Where(p => p.Productid == c.Productid)
                .ToListAsync();

            foreach (var pab in pabs)
            {

                if (pab.Basegood is not null && pab.Basegood.Suggestedprice is not null)
                {
                    total += (decimal)pab.Basegood!.Suggestedprice;
                }
                if (pab.Addon is not null && pab.Addon.Suggestedprice is not null)
                {
                    total += (decimal)pab.Addon!.Suggestedprice;
                }
            }
            if (c.Quantity is null || c.Quantity < 1)
            {
                c.Quantity = 1;
            }
            price += total * (decimal)c.Quantity;
        }

        //create a default purchase
        Purchase newPurchase = new()
        {
            Customerid = customerId,
            //need to generate correct price
            Orderdate = DateTime.Today,
            //have a way for them to input specifications to their order
            Isfulfilled = false,
            Actualprice = price, // Temp for now
            Fulfillmentdate = null //Have a way for her to fulfill orders
        };

        _context.Purchases.Add(newPurchase);
        await _context.SaveChangesAsync();

        //for each cart item, add the product id into purchase_product
        foreach (Cart cart in carts)
        {
            _context.PurchaseProducts.Add(new PurchaseProduct { Productid = cart.Productid, Purchaseid = newPurchase.Id, Quantity = cart.Quantity });
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        return newPurchase.Id;
    }
}