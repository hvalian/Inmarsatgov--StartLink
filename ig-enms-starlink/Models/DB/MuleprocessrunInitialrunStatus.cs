using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class MuleprocessrunInitialrunStatus
{
    public string? Processname { get; set; }

    public int? Nodeid { get; set; }

    public long? InitialstartTime { get; set; }

    public long? ProgressrunTime { get; set; }

    public long? InitialendTime { get; set; }

    public string? Running { get; set; }
}
