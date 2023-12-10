using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbDnsconfig
{
    public int Id { get; set; }

    public string PrimaryIp { get; set; } = null!;

    public string PrimaryServerName { get; set; } = null!;

    public string SecondaryIp { get; set; } = null!;

    public string SecondaryServerName { get; set; } = null!;

    public int TerminalsvnconfigId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? SddDnsconfigId { get; set; }

    public short? IsActive { get; set; }

    public virtual TbTerminalsvnConfig Terminalsvnconfig { get; set; } = null!;
}
