using System;
using System.Collections.Generic;

namespace KakeysBakery.Data;

public partial class ProductAddon
{
    public int Id { get; set; }

    public int? Productid { get; set; }

    public int? Addonid { get; set; }

    public virtual Addon? Addon { get; set; }

    public virtual Product? Product { get; set; }
}
