using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;

public partial class Addonflavor
{
    public string? Flavor { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
}