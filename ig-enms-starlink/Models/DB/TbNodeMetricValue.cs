﻿using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNodeMetricValue
{
    public long Id { get; set; }

    public int NodeId { get; set; }

    public int NodeMetricId { get; set; }

    public int NodeTypeId { get; set; }

    public string MetricKey { get; set; } = null!;

    public string MetricValueType { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Value { get; set; } = null!;

    public string? Metricvalue1 { get; set; }

    public string? Metricvalue2 { get; set; }

    public string? Metricvalue3 { get; set; }

    public long? EpochTimestamp { get; set; }

    public long LastPolledTimestamp { get; set; }

    public long? BackFillMetricValueId { get; set; }

    public bool? IsbackFilled { get; set; }

    public virtual TbNode Node { get; set; } = null!;

    public virtual TbNodeMetric NodeMetric { get; set; } = null!;
}
