using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;

public partial class BasegoodSize
{
    public int Id { get; set; }

    public string? Size { get; set; }

    public virtual ICollection<Basegood> Basegoods { get; set; } = new List<Basegood>();
}
