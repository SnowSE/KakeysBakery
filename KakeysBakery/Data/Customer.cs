using System;
using System.Collections.Generic;

namespace KakeysBakery.Data;

public partial class Customer
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Forename { get; set; }

    public string? Surname { get; set; }

    public int? Phone { get; set; }

    public string? Preferredcontact { get; set; }

    public bool? Issubscribed { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
