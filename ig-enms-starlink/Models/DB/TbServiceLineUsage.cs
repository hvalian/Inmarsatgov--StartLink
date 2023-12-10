using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbServiceLineUsage
{
    public string ServiceLineNumber { get; set; } = null!;

    public DateTime UsageTs { get; set; }

    public int UsageBucketId { get; set; }

    public double? TotalGb { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public string RecordType { get; set; } = null!;

    public virtual TbServiceLine ServiceLineNumberNavigation { get; set; } = null!;

    public virtual TbUsageBucket UsageBucket { get; set; } = null!;
}
