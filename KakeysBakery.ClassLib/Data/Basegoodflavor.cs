using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;

public partial class Basegoodflavor
{
    public int Id { get; set; }

    public string? Flavorname { get; set; }

    public virtual ICollection<Basegood> Basegoods { get; set; } = new List<Basegood>();
}
