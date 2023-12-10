using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbGxorderManagement
{
    public int Id { get; set; }

    public string ActionType { get; set; } = null!;

    public string? SddOrderId { get; set; }

    public string EnmsRefId { get; set; } = null!;

    public int? OrderStatusId { get; set; }

    public int? TerminalProvisionId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public bool? RetryFlag { get; set; }

    public string? UserName { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual TbProvisionStatus? OrderStatus { get; set; }

    public virtual ICollection<TbModifyOrderDetail> TbModifyOrderDetails { get; set; } = new List<TbModifyOrderDetail>();

    public virtual TbTerminalProvisionInfo? TerminalProvision { get; set; }
}
