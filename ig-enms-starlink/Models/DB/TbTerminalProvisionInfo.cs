using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalProvisionInfo
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Tpk { get; set; } = null!;

    public int TerminaltypeConfigId { get; set; }

    public string EnmsRefId { get; set; } = null!;

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public int SubscriptionId { get; set; }

    public int SiteId { get; set; }

    public int SiteFolderId { get; set; }

    public string? StatsMgmtProfile { get; set; }

    public string? MeetmePoint { get; set; }

    public DateTime? ActivationDate { get; set; }

    public int CleId { get; set; }

    public string? CustomTerminalName { get; set; }

    public short? IsActive { get; set; }

    public int? UtilizationTypeId { get; set; }

    public string? BillingAccountId { get; set; }

    public string? AddedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? PingConfig { get; set; }

    public virtual TbSddcle Cle { get; set; } = null!;

    public virtual TbSddsite Site { get; set; } = null!;

    public virtual TbSddsiteFolder SiteFolder { get; set; } = null!;

    public virtual ICollection<TbGxorderManagement> TbGxorderManagements { get; set; } = new List<TbGxorderManagement>();

    public virtual ICollection<TbGxprovisionLog> TbGxprovisionLogs { get; set; } = new List<TbGxprovisionLog>();

    public virtual ICollection<TbPortSwitchConfig> TbPortSwitchConfigs { get; set; } = new List<TbPortSwitchConfig>();

    public virtual ICollection<TbTerminalSummary> TbTerminalSummaries { get; set; } = new List<TbTerminalSummary>();

    public virtual ICollection<TbTerminalsvnConfig> TbTerminalsvnConfigs { get; set; } = new List<TbTerminalsvnConfig>();

    public virtual TbTerminalTypeConfig TerminaltypeConfig { get; set; } = null!;
}
