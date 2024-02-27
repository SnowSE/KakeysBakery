using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;
public partial class Referencephoto
{
    public int Id { get; set; }

    public int? Purchaseid { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Purchase? Purchase { get; set; }
}
