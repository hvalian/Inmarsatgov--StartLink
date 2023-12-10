using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class ViewGxterminalStatusBySvn
{
    public int Nodeid { get; set; }

    public string GxterminalId { get; set; } = null!;

    public string Nodestatus { get; set; } = null!;

    public DateTime? LastStatusTimestamp { get; set; }

    public string? Nodestatusdesc { get; set; }

    public string? Svn { get; set; }
}
