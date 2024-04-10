using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;
public partial class Customer
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Forename { get; set; }

    public string? Surname { get; set; }

    public string? Phone { get; set; }

    public string? Preferredcontact { get; set; }

    public bool? Issubscribed { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<CustomerRole> CustomerRoles { get; set; } = new List<CustomerRole>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
