﻿using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class VwNodeMetric
{
    public int Id { get; set; }

    public int NodeId { get; set; }

    public int NodeTypeId { get; set; }

    public string MetricKey { get; set; } = null!;

    public string MetricValueType { get; set; } = null!;

    public string? SampleValue { get; set; }

    public string? Description { get; set; }

    public string? MetricValue1Label { get; set; }

    public string? MetricValue2Label { get; set; }

    public string? MetricValue3Label { get; set; }

    public bool Active { get; set; }

    public int PollingInterval { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? MetricSourceInfo { get; set; }

    public string Name { get; set; } = null!;

    public string? MetricUnits { get; set; }

    public string? NodeMetricData { get; set; }
}
