using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;

public partial class Basegood
{
    public int Id { get; set; }

    public decimal? Suggestedprice { get; set; }

    public int? Pastryid { get; set; }

    public int? Flavorid { get; set; }

    public bool? Isavailable { get; set; }

    public int? Goodsize { get; set; }

    public virtual Basegoodflavor? Flavor { get; set; }

    public virtual BasegoodSize? GoodsizeNavigation { get; set; }

    public virtual Basegoodtype? Pastry { get; set; }

    public virtual ICollection<ProductAddonBasegood> ProductAddonBasegoods { get; set; } = new List<ProductAddonBasegood>();
}
