using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class GxassuranceMetricsRunTimestamp
{
    public string? ProcessName { get; set; }

    public string? LastRunTime { get; set; }

    public string? NodeId { get; set; }

    public string? Running { get; set; }

    public string? RunId { get; set; }

    public string? Currentrunstart { get; set; }

    public string? LastRunTimeEpoch { get; set; }

    public string? CurrentrunstartEpoch { get; set; }
}
