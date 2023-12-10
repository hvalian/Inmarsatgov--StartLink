using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSvnUdr
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public int? BeamId { get; set; }

    public string? Duplicate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? TotalTime { get; set; }

    public string? FramedIp { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? OriginHost { get; set; }

    public string? OriginRealm { get; set; }

    public int? RecordNumber { get; set; }

    public string? RecordType { get; set; }

    public int? RxBytes { get; set; }

    public int? TxBytes { get; set; }

    public int? TotalBytes { get; set; }

    public string? Sac { get; set; }

    public string? Subscriber { get; set; }

    public int? Svn { get; set; }

    public string? SatelliteId { get; set; }

    public int? ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? SessionId { get; set; }

    public int? Termination { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateProcessed { get; set; }
}
