using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbterminalenmsGroup
{
    public int TerminalgroupId { get; set; }

    public int TerminalprovisionId { get; set; }

    public int EnmsGroupId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual TbenmsGroup EnmsGroup { get; set; } = null!;
}
