using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbGxprovisionLog
{
    public int Id { get; set; }

    public string ActionType { get; set; } = null!;

    public string? EnmsRefId { get; set; }

    public string? Payload { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string LogType { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public int? TerminalProvisionId { get; set; }

    public string? SddApimodule { get; set; }

    public int? OrderMgmtId { get; set; }

    public int? ModifyOrderDetailId { get; set; }

    public virtual TbModifyOrderDetail? ModifyOrderDetail { get; set; }

    public virtual TbTerminalProvisionInfo? TerminalProvision { get; set; }
}
