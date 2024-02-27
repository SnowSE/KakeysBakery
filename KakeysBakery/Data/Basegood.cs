using System;
using System.Collections.Generic;

namespace KakeysBakery.Data;

public partial class Basegood
{
    public int Id { get; set; }

    public string? Basegoodname { get; set; }

    public string? Flavor { get; set; }

    public decimal? Suggestedprice { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
