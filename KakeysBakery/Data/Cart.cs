﻿using System;
using System.Collections.Generic;

namespace KakeysBakery.Data;

public partial class Cart
{
    public int Id { get; set; }

    public int? Customerid { get; set; }

    public int? Productid { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
