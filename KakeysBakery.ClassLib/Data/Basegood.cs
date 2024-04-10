using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;
public partial class Basegood
{
    public int Id { get; set; }

    public decimal? Suggestedprice { get; set; }

    public int? Typeid { get; set; }

    public int? Flavorid { get; set; }

    public bool? Isavailable { get; set; }

    public int? Sizeid { get; set; }

    public virtual Basegoodflavor? Flavor { get; set; }

    public virtual ICollection<ProductAddonBasegood> ProductAddonBasegoods { get; set; } = new List<ProductAddonBasegood>();

    public virtual BasegoodSize? Size { get; set; }

    public virtual Basegoodtype? Type { get; set; }
}
