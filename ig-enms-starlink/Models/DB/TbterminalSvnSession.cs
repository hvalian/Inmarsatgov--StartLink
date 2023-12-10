using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbterminalSvnSession
{
    public int Id { get; set; }

    public int NodeId { get; set; }

    public string Subscriber { get; set; } = null!;

    public string Svn { get; set; } = null!;

    public DateTime SessionStart { get; set; }

    public DateTime? SessionEnd { get; set; }

    public short? SessionEndFlag { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? Dateupdated { get; set; }
}
