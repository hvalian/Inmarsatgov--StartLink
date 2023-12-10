using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbPortSwitchConfig
{
    public int Id { get; set; }

    public string PortSwitchNumber { get; set; } = null!;

    public string AutoNegotiation { get; set; } = null!;

    public string Speed { get; set; } = null!;

    public string Mode { get; set; } = null!;

    public string? Tagenable { get; set; }

    public string? EnableTrunk { get; set; }

    public int? TerminalProvisionInfoId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? Svnlist { get; set; }

    public string? SddPortConfigId { get; set; }

    public short? IsActive { get; set; }

    public string? TrunkSvnlist { get; set; }

    public string? PortMode { get; set; }

    public virtual TbTerminalProvisionInfo? TerminalProvisionInfo { get; set; }
}
