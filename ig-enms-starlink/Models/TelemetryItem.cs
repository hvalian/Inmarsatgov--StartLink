// File Name: TelemetryItem.cs
// Author: rameshvishnubhatla
// Date Created: 8/16/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class TelemetryItem
	{
		public string DeviceType { get; set; } = string.Empty;
		private long utcTimestampNs;

		public long UtcTimestampNs {
			get => utcTimestampNs;
			set {
				utcTimestampNs = value;
				TimeStamp = (new DateTime(1970, 1, 1).AddMilliseconds(utcTimestampNs / 1000000));
			}
		}

		private string deviceId;

		public string DeviceId {
			get => deviceId;
			set {
				deviceId = value;
				TerminalId =  (deviceId ==null) ? "" :deviceId.Replace("ut", "");
			}
		}
		public double DownlinkThroughput { get; set; }
		public double UplinkThroughput { get; set; }
		public double PingDropRateAvg { get; set; }
		public int PingLatencyMsAvg { get; set; }
		public double ObstructionPercentTime { get; set; }
		public int Uptime { get; set; }
		public double SignalQuality { get; set; }
		public List<string> Alerts = new List<string>();

		public string TerminalId { get; set; } = string.Empty;
		public DateTime TimeStamp { get; set; }
	}
}

