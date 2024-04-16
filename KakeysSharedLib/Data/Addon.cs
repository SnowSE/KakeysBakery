using System;
using System.Collections.Generic;

namespace KakeysSharedLib.Data;

public partial class Addon
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal? Suggestedprice { get; set; }

    public int? Addontypeid { get; set; }

    public int? Addonflavorid { get; set; }

    public virtual Addonflavor? Addonflavor { get; set; }

    public virtual Addontype? Addontype { get; set; }

    public virtual ICollection<ProductAddonBasegood> ProductAddonBasegoods { get; set; } = new List<ProductAddonBasegood>();
}
