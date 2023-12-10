using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbCustomer
{
    public int Id { get; set; }

    public string ServiceNowId { get; set; } = null!;

    public string SegAid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int IsDeleted { get; set; }

    public DateTime DateCreated { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime DateUpdated { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }

    public string? ExternalId { get; set; }
}
