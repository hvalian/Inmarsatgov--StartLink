using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbServiceLineSubscriptionMap
{
    public int SlsubscriptionMapId { get; set; }

    public string ServiceLineNumber { get; set; } = null!;

    public string? SubscriptionRefId { get; set; }

    public string? ProductRefId { get; set; }

    public DateTime? ServiceStart { get; set; }

    public DateTime? ServiceEnd { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public string? OverageName { get; set; }

    public string? OverageDescription { get; set; }

    public bool? IsOptedIntoOverage { get; set; }

    public double? PricePerGb { get; set; }

    public int? UsageLimitGb { get; set; }

    public double? OverageAmountGb { get; set; }

    public double? OveragePrice { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual TbProduct? ProductRef { get; set; }

    public virtual TbServiceLine ServiceLineNumberNavigation { get; set; } = null!;
}
