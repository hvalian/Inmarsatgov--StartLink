using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSddsubscriptionPlan
{
    public int Id { get; set; }

    public int ProductOfferingId { get; set; }

    public string ProductSpecificationId { get; set; } = null!;

    public string ProductSpecificationName { get; set; } = null!;

    public int SubscriptionId { get; set; }

    public string SubscriptionName { get; set; } = null!;

    public string Characteristicname { get; set; } = null!;

    public string? Characteristicvalue { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public short? IsActive { get; set; }

    public virtual TbSddproductOffering ProductOffering { get; set; } = null!;
}
