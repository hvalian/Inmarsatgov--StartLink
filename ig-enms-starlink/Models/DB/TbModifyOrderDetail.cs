using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbModifyOrderDetail
{
    public int Id { get; set; }

    public int? OrderMgmtId { get; set; }

    public int ModifyOrderTypeId { get; set; }

    public int? OrderStatusId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? SddOrderId { get; set; }

    public virtual TbGxLookup ModifyOrderType { get; set; } = null!;

    public virtual TbGxorderManagement? OrderMgmt { get; set; }

    public virtual TbProvisionStatus? OrderStatus { get; set; }

    public virtual ICollection<TbGxprovisionLog> TbGxprovisionLogs { get; set; } = new List<TbGxprovisionLog>();
}
