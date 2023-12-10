using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbLog
{
    public long Id { get; set; }

    public string LogType { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string MethodName { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public int ElapsedTime { get; set; }

    public string? Message { get; set; }

    public int? AffectedRows { get; set; }

    public string? ExceptionMessage { get; set; }

    public string? InnerException { get; set; }

    public string? StackTrace { get; set; }
}
