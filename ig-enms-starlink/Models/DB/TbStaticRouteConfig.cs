using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbStaticRouteConfig
{
    public int Id { get; set; }

    public string Interface { get; set; } = null!;

    public string Ipv4Address { get; set; } = null!;

    public string Ipv4Gateway { get; set; } = null!;

    public string Ipv4Netmask { get; set; } = null!;

    public int TerminalsvnconfigId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? SddStaticRouteConfigId { get; set; }

    public short? IsActive { get; set; }

    public virtual TbTerminalsvnConfig Terminalsvnconfig { get; set; } = null!;
}
