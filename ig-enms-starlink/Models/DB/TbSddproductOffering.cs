using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSddproductOffering
{
    public int Id { get; set; }

    public string PdtOfferingId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public int? Contractid { get; set; }

    public virtual TbSddcontract? Contract { get; set; }

    public virtual ICollection<TbSddsubscriptionPlan> TbSddsubscriptionPlans { get; set; } = new List<TbSddsubscriptionPlan>();
}
