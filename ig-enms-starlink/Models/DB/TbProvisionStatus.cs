using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbProvisionStatus
{
    public int Statusid { get; set; }

    public string StatusDesc { get; set; } = null!;

    public string? Sddstatusdesc { get; set; }

    public virtual ICollection<TbGxorderManagement> TbGxorderManagements { get; set; } = new List<TbGxorderManagement>();

    public virtual ICollection<TbModifyOrderDetail> TbModifyOrderDetails { get; set; } = new List<TbModifyOrderDetail>();
}
