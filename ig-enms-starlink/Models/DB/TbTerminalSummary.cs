using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalSummary
{
    public int Id { get; set; }

    public string EnmsRefId { get; set; } = null!;

    public string GxTerminalId { get; set; } = null!;

    public string TerminalName { get; set; } = null!;

    public string TerminalType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string TerminalDid { get; set; } = null!;

    public string CoreModuleId { get; set; } = null!;

    public string ServiceDeliveryPointId { get; set; } = null!;

    public DateTime? ActivationDate { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public int? NodeId { get; set; }

    public string? SddInstanceId { get; set; }

    public string? SerialNumber { get; set; }

    public int TerminalProvisionId { get; set; }

    public string? SddGxconfigId { get; set; }

    public string? SddtempdeactivationConfigId { get; set; }

    public string? SddGxid { get; set; }

    public string? SddGxsatelliteTerminalId { get; set; }

    public string? SddcustomerProvidedTerminalId { get; set; }

    public string? SddgxsatelliteTerminalTypeId { get; set; }

    public string? SddGxtpsid { get; set; }

    public string? SdtServiceDeliveryPointId { get; set; }

    public string? ActiveFirmware { get; set; }

    public string? BackupFirmware { get; set; }

    public virtual TbNode? Node { get; set; }

    public virtual TbTerminalProvisionInfo TerminalProvision { get; set; } = null!;
}
