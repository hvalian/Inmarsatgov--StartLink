using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalTypeConfig
{
    public int Id { get; set; }

    public string TerminalType { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? SddproductItemconfig { get; set; }

    public int? Portcount { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TbTerminalFirmwareConfig> TbTerminalFirmwareConfigs { get; set; } = new List<TbTerminalFirmwareConfig>();

    public virtual ICollection<TbTerminalProvisionInfo> TbTerminalProvisionInfos { get; set; } = new List<TbTerminalProvisionInfo>();
}
