using System;
using System.Collections.Generic;

namespace KakeysSharedlib.Data;

public partial class ProductAddonBasegood
{
    public int Id { get; set; }

    public int? Productid { get; set; }

    public int? Addonid { get; set; }

    public int? Basegoodid { get; set; }

    public virtual Addon? Addon { get; set; }

    public virtual Basegood? Basegood { get; set; }

    public virtual Product? Product { get; set; }
}
