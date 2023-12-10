using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class VwActiveTerminalInfo
{
    public string? TerminalId { get; set; }

    public string? TerminalName { get; set; }

    public string? NetworkName { get; set; }

    public string? TerminalSerialNumber { get; set; }

    public string? TerminalDid { get; set; }

    public int NodeId { get; set; }
}
