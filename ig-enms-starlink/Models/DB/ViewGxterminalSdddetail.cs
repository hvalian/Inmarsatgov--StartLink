using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class ViewGxterminalSdddetail
{
    public string? Terminalname { get; set; }

    public string Customerid { get; set; } = null!;

    public string? Customer { get; set; }

    public string? TerminalType { get; set; }

    public string Gxterminalid { get; set; } = null!;

    public string ProvStatus { get; set; } = null!;

    public int ProvStatusId { get; set; }

    public int? Enmsnodeid { get; set; }

    public string? MeetmePoint { get; set; }

    public string Var { get; set; } = null!;

    public string CoreModuleId { get; set; } = null!;

    public string TerminalDid { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public string Manufacturer { get; set; } = null!;

    public string Tpk { get; set; } = null!;

    public string? TerminalRmp { get; set; }

    public int? UtilizationTypeId { get; set; }

    public int TerminalProvisionId { get; set; }

    public string? SddInstanceId { get; set; }

    public string? SdtServiceDeliveryPointId { get; set; }

    public int SiteId { get; set; }

    public int Cleid { get; set; }

    public int? VarId { get; set; }

    public DateTime? ActivationDate { get; set; }
}
