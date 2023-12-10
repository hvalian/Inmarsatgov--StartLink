using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class ViewSpeedcastTerminalStatus
{
    public int Nodeid { get; set; }

    public string NodeName { get; set; } = null!;

    public string Nodestatus { get; set; } = null!;

    public DateTime? LastStatusTimestamp { get; set; }
}
