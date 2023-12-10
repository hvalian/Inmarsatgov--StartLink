using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbServiceLine
{
    public string ServiceLineNumber { get; set; } = null!;

    public string? AccountNumber { get; set; }

    public string? AddressReferenceId { get; set; }

    public string? Name { get; set; }

    public bool Active { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool IsTerminalRecord { get; set; }

    public virtual TbAddress? AddressReference { get; set; }

    public virtual ICollection<TbServiceLineSubscriptionMap> TbServiceLineSubscriptionMaps { get; set; } = new List<TbServiceLineSubscriptionMap>();

    public virtual ICollection<TbServiceLineUsage> TbServiceLineUsages { get; set; } = new List<TbServiceLineUsage>();

    public virtual ICollection<TbTerminalServiceLineMap> TbTerminalServiceLineMaps { get; set; } = new List<TbTerminalServiceLineMap>();
}
