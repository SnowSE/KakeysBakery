using System;
using System.Collections.Generic;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;

namespace KakeysBakeryClassLib.Data;

public partial class Addon
{
    public int Id { get; set; }

    public string? Flavor { get; set; }

    public string? Description { get; set; }

    public decimal? Suggestedprice { get; set; }

    public string? Addontypename { get; set; }

    [ManyToOne]
    public virtual ICollection<ProductAddon> ProductAddons { get; set; } = new List<ProductAddon>();
}
