using System;
using System.Collections.Generic;

namespace KakeysBakeryClassLib.Data;
public partial class Purchase
{
    public int Id { get; set; }

    public int? Customerid { get; set; }

    public decimal? Actualprice { get; set; }

    public DateTime? Orderdate { get; set; }

    public string? Specifications { get; set; }

    public bool? Isfulfilled { get; set; }

    public DateTime? Fulfillmentdate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; } = new List<PurchaseProduct>();

    public virtual ICollection<Referencephoto> Referencephotos { get; set; } = new List<Referencephoto>();
}
