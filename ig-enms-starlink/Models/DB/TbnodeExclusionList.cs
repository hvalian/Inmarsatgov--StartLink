using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbnodeExclusionList
{
    public string? GxTerminalId { get; set; }

    public int? ConnectorId { get; set; }

    public DateTime? Dateadded { get; set; }
}
