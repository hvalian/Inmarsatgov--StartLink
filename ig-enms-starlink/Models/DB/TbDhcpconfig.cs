using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbDhcpconfig
{
    public int Id { get; set; }

    public string? Dhcpmode { get; set; }

    public string? Relaysubnetmask { get; set; }

    public string? Relaytoipaddress { get; set; }

    public string? Relaysubnetipaddress { get; set; }

    public string? Primaryserveripadddress { get; set; }

    public string? Secserveripaddress { get; set; }

    public string? Leasedurationunit { get; set; }

    public int? Leaseduration { get; set; }

    public string? Maxleaselengthinsec { get; set; }

    public string? Serversubnetipaddress { get; set; }

    public string? Serversubnetmask { get; set; }

    public string? Defaultgatewayipaddress { get; set; }

    public string? Iprangestart { get; set; }

    public string? Iprangeend { get; set; }

    public string? Broadcastipaddress { get; set; }

    public int? TerminalsvnconfigId { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public string? SddDhcpconfigId { get; set; }

    public short? IsActive { get; set; }

    public string? SddDhcpparentConfigId { get; set; }

    public virtual TbTerminalsvnConfig? Terminalsvnconfig { get; set; }
}
