using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSddsite
{
    public int Id { get; set; }

    public int SiteId { get; set; }

    public string Name { get; set; } = null!;

    public int SiteFolderId { get; set; }

    public string Status { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? InstallationCountry { get; set; }

    public string? Class { get; set; }

    public string? TailNumber { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public short? IsActive { get; set; }

    public virtual TbSddsiteFolder SiteFolder { get; set; } = null!;

    public virtual ICollection<TbTerminalProvisionInfo> TbTerminalProvisionInfos { get; set; } = new List<TbTerminalProvisionInfo>();
}
