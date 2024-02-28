using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;
public partial class Product
{
    public int Id { get; set; }

    public int? Basegoodid { get; set; }

    public string? Description { get; set; }

    public string? Productname { get; set; }

    public bool? Ispublic { get; set; }

    public virtual Basegood? Basegood { get; set; }

    [ManyToOne]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ManyToOne]
    public virtual ICollection<ProductAddon> ProductAddons { get; set; } = new List<ProductAddon>();

    [ManyToOne]
    public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; } = new List<PurchaseProduct>();
}
