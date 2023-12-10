using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbProduct
{
    public string ProductReferenceId { get; set; } = null!;

    public string? Name { get; set; }

    public double? Price { get; set; }

    public string? PriceCurrency { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual ICollection<TbServiceLineSubscriptionMap> TbServiceLineSubscriptionMaps { get; set; } = new List<TbServiceLineSubscriptionMap>();
}
