using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public int NodeTypeId { get; set; }

    public int ConnectorId { get; set; }

    public string? ConnectorCreds { get; set; }

    public string? SourceInfo { get; set; }

    public int PollerId { get; set; }

    public bool? UseMetricTemplate { get; set; }

    public int? NodeTypeTemplateId { get; set; }

    public int PollingInterval { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? PollPolicy { get; set; }

    public DateTime? LastPollDate { get; set; }

    public DateTime? NextPollDate { get; set; }

    public string? NodeData { get; set; }

    public string? ProcessorName { get; set; }

    public DateTime? ActivationDate { get; set; }

    public DateTime? DeactivationDate { get; set; }

    public virtual TbConnector Connector { get; set; } = null!;

    public virtual TbNodeType NodeType { get; set; } = null!;

    public virtual TbNodeTypeTemplate? NodeTypeTemplate { get; set; }

    public virtual TbPoller Poller { get; set; } = null!;

    public virtual ICollection<TbFisasvnSession> TbFisasvnSessions { get; set; } = new List<TbFisasvnSession>();

    public virtual ICollection<TbNodeConfigValue> TbNodeConfigValues { get; set; } = new List<TbNodeConfigValue>();

    public virtual ICollection<TbNodeConfig> TbNodeConfigs { get; set; } = new List<TbNodeConfig>();

    public virtual ICollection<TbNodeMetricValue> TbNodeMetricValues { get; set; } = new List<TbNodeMetricValue>();

    public virtual ICollection<TbNodeMetric> TbNodeMetrics { get; set; } = new List<TbNodeMetric>();

    public virtual ICollection<TbNodeParent> TbNodeParentNodes { get; set; } = new List<TbNodeParent>();

    public virtual ICollection<TbNodeParent> TbNodeParentParentnodes { get; set; } = new List<TbNodeParent>();

    public virtual ICollection<TbSubscriberServicePlan> TbSubscriberServicePlans { get; set; } = new List<TbSubscriberServicePlan>();

    public virtual ICollection<TbTerminalExternalCustomerMap> TbTerminalExternalCustomerMaps { get; set; } = new List<TbTerminalExternalCustomerMap>();

    public virtual ICollection<TbTerminalServiceLineMap> TbTerminalServiceLineMaps { get; set; } = new List<TbTerminalServiceLineMap>();

    public virtual ICollection<TbTerminalServicePlan> TbTerminalServicePlans { get; set; } = new List<TbTerminalServicePlan>();

    public virtual ICollection<TbTerminalSummary> TbTerminalSummaries { get; set; } = new List<TbTerminalSummary>();

    public virtual ICollection<TmptbNodeMetricValueWithBackfill> TmptbNodeMetricValueWithBackfills { get; set; } = new List<TmptbNodeMetricValueWithBackfill>();
}
