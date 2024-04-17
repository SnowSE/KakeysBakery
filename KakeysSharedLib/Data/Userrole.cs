using System;
using System.Collections.Generic;

namespace KakeysSharedLib.Data;

public partial class Userrole
{
    public int Id { get; set; }

    public string Userrole1 { get; set; } = null!;

    public virtual ICollection<CustomerRole> CustomerRoles { get; set; } = new List<CustomerRole>();
}