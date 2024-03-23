using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;

public partial class CustomerRole
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? UserroleId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Userrole? Userrole { get; set; }
}
