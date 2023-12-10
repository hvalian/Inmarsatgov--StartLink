using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class VwTelemetry
{
    public int NodeId { get; set; }

    public double? DownlinkThroughput { get; set; }

    public double? ObstructionPercentTime { get; set; }

    public double? PingDropRateAvg { get; set; }

    public double? PingLatencyMsAvg { get; set; }

    public double? SignalQuality { get; set; }

    public double? UplinkThroughput { get; set; }

    public double? Uptime { get; set; }

    public DateTime Timestamp { get; set; }

    public long? EpochTimestamp { get; set; }
}
