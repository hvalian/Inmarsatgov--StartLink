using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNatpatconfig
{
    public int Id { get; set; }

    public string? Timeout { get; set; }

    public string? FirewallEnable { get; set; }

    public string? FirewallPortRange { get; set; }

    public string? FirewallProtocol { get; set; }

    public string? SessionLocalAddress { get; set; }

    public string? SessionLocalPortRange { get; set; }

    public string? SessionNatportRange { get; set; }

    public string? SessionProtocol { get; set; }

    public string? SipalgtableEnable { get; set; }

    public string? SipalgtablePort { get; set; }

    public int TerminalsvnconfigId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? SddNatpatconfigId { get; set; }

    public short? IsActive { get; set; }

    public virtual TbTerminalsvnConfig Terminalsvnconfig { get; set; } = null!;
}
