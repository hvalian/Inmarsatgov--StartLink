using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbGxLookup
{
    public int Id { get; set; }

    public string MetricKey { get; set; } = null!;

    public string MetricDesc { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<TbModifyOrderDetail> TbModifyOrderDetails { get; set; } = new List<TbModifyOrderDetail>();
}
