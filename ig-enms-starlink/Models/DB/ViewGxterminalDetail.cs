using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class ViewGxterminalDetail
{
    public int NodeId { get; set; }

    public string GxterminalId { get; set; } = null!;

    public string? TerminalServicePlan { get; set; }

    public string? TerminalName { get; set; }

    public bool Active { get; set; }

    public string? TerminalStatus { get; set; }

    public string TerminalStatusdesc { get; set; } = null!;

    public DateTime? LastStatusTimestamp { get; set; }

    public string NodeType { get; set; } = null!;

    public int? ConnectorId { get; set; }

    public string? Connector { get; set; }
}
