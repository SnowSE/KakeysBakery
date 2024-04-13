using System;
using System.Collections.Generic;

namespace KakeysSharedLib.Data;
public partial class PurchaseProduct
{
    public int Id { get; set; }

    public int? Purchaseid { get; set; }

    public int? Productid { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Purchase? Purchase { get; set; }
}