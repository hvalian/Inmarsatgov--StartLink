using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class ViewTerminalSvnsession
{
    public int NodeId { get; set; }

    public string Svn { get; set; } = null!;

    public DateTime SessionStart { get; set; }

    public DateTime? SessionEnd { get; set; }

    public string Source { get; set; } = null!;
}
