using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalsvnConfig
{
    public int Id { get; set; }

    public int Svnid { get; set; }

    public string Ipaddress { get; set; } = null!;

    public string Subnet { get; set; } = null!;

    public int? TerminalProvisionInfoId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? SddsvnConfigId { get; set; }

    public short? IsActive { get; set; }

    public string? Sat0Address { get; set; }

    public virtual TbSddsvn Svn { get; set; } = null!;

    public virtual ICollection<TbBgpconfig> TbBgpconfigs { get; set; } = new List<TbBgpconfig>();

    public virtual ICollection<TbDhcpconfig> TbDhcpconfigs { get; set; } = new List<TbDhcpconfig>();

    public virtual ICollection<TbDnsconfig> TbDnsconfigs { get; set; } = new List<TbDnsconfig>();

    public virtual ICollection<TbNatpatconfig> TbNatpatconfigs { get; set; } = new List<TbNatpatconfig>();

    public virtual ICollection<TbStaticRouteConfig> TbStaticRouteConfigs { get; set; } = new List<TbStaticRouteConfig>();

    public virtual TbTerminalProvisionInfo? TerminalProvisionInfo { get; set; }
}
