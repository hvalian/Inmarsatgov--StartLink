// File Name: TelemetryModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/9/2023
//
//
using System;
namespace IG.ENMS.Models.Starlink.NMS
{
	public class TelemetryModel
	{
		DateTime timestamp;
		long utcTimestamp = 0;
		double DownlinkThroughput { get; set; }
		double UplinkThroughput { get; set; }
		double PingDropRateAvg { get; set; }
		int PingLatencyMsAvg { get; set; }
		double ObstructionPercentTime { get; set; }
		int Uptime { get; set; }
		double SignalQuality { get; set; }

		public DateTime TimeStamp {
			get {
				return timestamp;
			}
		}
		
		public long UtcTimestamp { get => utcTimestamp; set { utcTimestamp = value;
				DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(utcTimestamp / 1000000000);
				timestamp = dateTimeOffset.DateTime;
			} }


	}
}

