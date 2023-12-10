using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSnterminal
{
    public string SysId { get; set; } = null!;

    public string GxTerminalId { get; set; } = null!;

    public string? TerminalName { get; set; }

    public string? Tpk { get; set; }

    public string? CoreModuleId { get; set; }

    public string? TerminalRmp { get; set; }

    public DateTime? ProvisionedDate { get; set; }

    public bool? GetStats { get; set; }

    public int? TerminalStatus { get; set; }

    public string? TerminalType { get; set; }

    public string? Manufacturer { get; set; }

    public string? CompanyId { get; set; }

    public string? Company { get; set; }

    public DateTime? DateImported { get; set; }
}
