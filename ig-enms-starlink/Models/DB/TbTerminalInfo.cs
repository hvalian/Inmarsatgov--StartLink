using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalInfo
{
    public int Id { get; set; }

    public string TerminalId { get; set; } = null!;

    public string TerminalName { get; set; } = null!;

    public string NetworkName { get; set; } = null!;

    public string TerminalSerialNumber { get; set; } = null!;

    public string TerminalDid { get; set; } = null!;

    public int NodeId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }
}
