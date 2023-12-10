using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalUtilizationLookup
{
    public int Id { get; set; }

    public string UtilizationType { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public bool? IsActive { get; set; }
}
