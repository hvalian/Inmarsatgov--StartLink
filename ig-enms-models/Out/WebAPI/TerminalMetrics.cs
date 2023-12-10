// File Name: TerminalMetrics.cs
// Author: rameshvishnubhatla
// Date Created: 8/30/2023
//
//
using System;
using static IG.ENMS.Models.Starlink.Out.WebAPI.TerminalUsage;

namespace IG.ENMS.Models.Starlink.Out.WebAPI
{
	public class TerminalMetrics
	{
		public List<TerminalMetricsRecord> TerminalMetricsList;

		public TerminalMetrics()
		{
			TerminalMetricsList = new List<TerminalMetricsRecord>();
		}

	}

	public class TerminalMetricsRecord
	{
		public string TerminalId { get; set; } = string.Empty;
		public DateTime timestamp { get; set; }

		public double DownlinkThroughput { get; set; }
		public double UplinkThroughput { get; set; }
		public double PingDropRateAvg { get; set; }
		public int PingLatencyMsAvg { get; set; }
		public double ObstructionPercentTime { get; set; }
		public int Uptime { get; set; }
		public double SignalQuality { get; set; }
	}
}

