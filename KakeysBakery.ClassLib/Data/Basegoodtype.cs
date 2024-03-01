using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;

public partial class Basegoodtype
{
    public int Id { get; set; }

    public string? Basegood { get; set; }

    public virtual ICollection<Basegood> Basegoods { get; set; } = new List<Basegood>();
}
