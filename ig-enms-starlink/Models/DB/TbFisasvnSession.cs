using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbFisasvnSession
{
    public int SvnSessionId { get; set; }

    public int NodeId { get; set; }

    public int SvnId { get; set; }

    public DateTime SessionStart { get; set; }

    public DateTime? SessionEnd { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? Dateupdated { get; set; }

    public string? AddedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DateProcessed { get; set; }

    public virtual TbNode Node { get; set; } = null!;

    public virtual TbSddsvn Svn { get; set; } = null!;
}
