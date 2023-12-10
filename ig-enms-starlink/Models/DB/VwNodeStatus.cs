using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class VwNodeStatus
{
    public string? UserTerminalId { get; set; }

    public bool? Active { get; set; }
}
