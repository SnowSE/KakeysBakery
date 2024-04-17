using System;
using System.Collections.Generic;

namespace KakeysSharedLib.Data;

public partial class Cart
{
    public int Id { get; set; }

    public int? Customerid { get; set; }

    public int? Productid { get; set; }

    public int? Quantity { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}