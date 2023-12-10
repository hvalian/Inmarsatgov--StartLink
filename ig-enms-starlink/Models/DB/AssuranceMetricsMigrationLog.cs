using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class AssuranceMetricsMigrationLog
{
    public DateTime? Startdatetime { get; set; }

    public DateTime? Enddatetime { get; set; }

    public int? Recordcount { get; set; }

    public string? Log { get; set; }

    public DateTime? Dateadded { get; set; }
}
