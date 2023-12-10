using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class VwTerminal
{
    public string? UserTerminalId { get; set; }

    public string? KitSerialNumber { get; set; }

    public string? DishSerialNumber { get; set; }

    public string ServiceLineNumber { get; set; } = null!;

    public DateTime? DeactivationDate { get; set; }

    public bool? Active { get; set; }
}
