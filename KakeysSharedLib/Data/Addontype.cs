using System;
using System.Collections.Generic;

namespace KakeysSharedlib.Data;

public partial class Addontype
{
    public int Id { get; set; }

    public string? Basetype { get; set; }

    public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
}
