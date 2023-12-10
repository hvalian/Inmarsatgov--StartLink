using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbUsageBucket
{
    public int UsageBucketid { get; set; }

    public string? Name { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual ICollection<TbServiceLineUsage> TbServiceLineUsages { get; set; } = new List<TbServiceLineUsage>();
}
