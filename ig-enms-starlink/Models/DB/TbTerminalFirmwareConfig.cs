using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalFirmwareConfig
{
    public int Id { get; set; }

    public int TerminalTypeId { get; set; }

    public string? ModifyOrderType { get; set; }

    public string? ActiveFirmware { get; set; }

    public string? BackupFirmware { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public bool? IsActive { get; set; }

    public virtual TbTerminalTypeConfig TerminalType { get; set; } = null!;
}
