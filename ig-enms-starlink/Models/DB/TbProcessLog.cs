using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbProcessLog
{
    public int ProcessId { get; set; }

    public string ProcessName { get; set; } = null!;

    public int ConnectorId { get; set; }

    public DateTime RequestDate { get; set; }

    public string? Request { get; set; }

    public string? Response { get; set; }

    public string? ResponseCode { get; set; }

    public string? ErrorType { get; set; }

    public DateTime LogDate { get; set; }
}
