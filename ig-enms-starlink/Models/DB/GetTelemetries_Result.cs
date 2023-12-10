namespace IG.ENMS.Starlink.Models.DB
{
    public class GetTelemetries_Result
    {
        public double DownlinkThroughput { get; set; }
        public double ObstructionPercentTime { get; set; }
        public double PingDropRateAvg { get; set; }
        public int PingLatencyMsAvg { get; set; }
        public double SignalQuality { get; set; }
        public string TerminalId { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
        public double UplinkThroughput { get; set; }
        public long UtcTimestampNs { get; set; }
        public int Uptime { get; set; }
    }
}
