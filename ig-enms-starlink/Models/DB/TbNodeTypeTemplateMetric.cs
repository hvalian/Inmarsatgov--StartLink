using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNodeTypeTemplateMetric
{
    public int Id { get; set; }

    public int NodeTypeTemplateId { get; set; }

    public int NodeTypeMetricId { get; set; }

    public int NodeTypeId { get; set; }

    public string MetricKey { get; set; } = null!;

    public bool? Active { get; set; }

    public int PollingInterval { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual TbNodeTypeMetric NodeTypeMetric { get; set; } = null!;

    public virtual TbNodeTypeTemplate NodeTypeTemplate { get; set; } = null!;
}
