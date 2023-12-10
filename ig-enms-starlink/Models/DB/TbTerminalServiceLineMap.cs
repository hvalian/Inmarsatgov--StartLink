using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalServiceLineMap
{
    public int TerminalServiceLineMapId { get; set; }

    public string ServiceLineNumber { get; set; } = null!;

    public int? NodeId { get; set; }

    public DateTime? ActivationDate { get; set; }

    public DateTime? DeactivationDate { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual TbNode? Node { get; set; }

    public virtual TbServiceLine ServiceLineNumberNavigation { get; set; } = null!;
}
