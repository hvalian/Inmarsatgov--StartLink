using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class VwSubscription
{
    public string? AccountNumber { get; set; }

    public string ServiceLineNumber { get; set; } = null!;

    public string? SubscriptionRefId { get; set; }

    public string? ProductRefId { get; set; }

    public DateTime? ServiceStart { get; set; }

    public DateTime? ServiceEnd { get; set; }
}
