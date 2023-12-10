using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNodeType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? Config { get; set; }

    public virtual ICollection<TbNodeConfigValue> TbNodeConfigValues { get; set; } = new List<TbNodeConfigValue>();

    public virtual ICollection<TbNodeConfig> TbNodeConfigs { get; set; } = new List<TbNodeConfig>();

    public virtual ICollection<TbNodeTypeMetric> TbNodeTypeMetrics { get; set; } = new List<TbNodeTypeMetric>();

    public virtual ICollection<TbNodeTypeTemplate> TbNodeTypeTemplates { get; set; } = new List<TbNodeTypeTemplate>();

    public virtual ICollection<TbNode> TbNodes { get; set; } = new List<TbNode>();
}
