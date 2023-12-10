// File Name: TelemetryItems.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System;
using System.Collections;
using System.Security.Principal;
using IG.ENMS.Models.Starlink.Out.WebAPI;
using IG.ENMS.Starlink.Models;

namespace IG.ENMS.Starlink.Data
{
	public class TelemetryItems : IEnumerable<Dictionary<DateTime, TelemetryItem>>
	{
        private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
        private readonly Settings _settings;
		private Dictionary<string, Dictionary<DateTime, TelemetryItem>> _telemetryItemList = new Dictionary<string, Dictionary<DateTime, TelemetryItem>>();

        public TelemetryItems(IConfiguration configuration, ILogger logger, Settings settings)
		{
            _configuration = configuration;
            _logger = logger;
			_settings = settings;
		}

		public bool Add(TelemetryItem TelemetryItem)
		{
			_logger.LogDebug("Entering Add(TelemetryItem) with argument {TelemetryItem}.", TelemetryItem);
			try {
				if (_telemetryItemList.ContainsKey(TelemetryItem.TerminalId)) {
					if (_telemetryItemList[TelemetryItem.TerminalId].ContainsKey(TelemetryItem.TimeStamp)) {
						_logger.LogDebug("TelemetryItem {TimeStamp} already exists.  Replacing it with new data.", TelemetryItem.TimeStamp);
						_telemetryItemList[TelemetryItem.TerminalId][TelemetryItem.TimeStamp] = TelemetryItem;
						return true;
					}
					else {
						_telemetryItemList[TelemetryItem.TerminalId].Add(TelemetryItem.TimeStamp, TelemetryItem);
						return true;
					}
				}

				_telemetryItemList.Add(TelemetryItem.TerminalId, new Dictionary<DateTime, TelemetryItem>());
				_telemetryItemList[TelemetryItem.TerminalId].Add(TelemetryItem.TimeStamp, TelemetryItem);

				_logger.LogDebug("TelemetryItem {TelemetryItem} added successfully.", TelemetryItem);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding TelemetryItem. {TelemetryItem}. Error: {errorMessage}", TelemetryItem, _Ex.Message);

				return false;
			}
		}

		public bool Remove(TelemetryItem TelemetryItem)
		{
			try {
				if (_telemetryItemList.ContainsKey(TelemetryItem.TerminalId)) {
					if (_telemetryItemList[TelemetryItem.TerminalId].ContainsKey(TelemetryItem.TimeStamp)) {
						_telemetryItemList[TelemetryItem.TerminalId].Remove(TelemetryItem.TimeStamp);
						return true;
					} else
						_logger.LogWarning("TelemetryItem {TimeStamp} for {TerminalId} does not exist to be removed.", TelemetryItem.TimeStamp, TelemetryItem.TerminalId);
				}

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing TelemetryItem for {TerminalId}. {TelemetryItem}. Error: {errorMessage}", TelemetryItem.TerminalId, TelemetryItem, _Ex.Message);

				return false;
			}
		}

		public Dictionary<DateTime, TelemetryItem> Get (string terminalId)
		{
			if (_telemetryItemList.ContainsKey(terminalId))
				return _telemetryItemList[terminalId];
			else
				return null;
		}

        public TerminalMetrics Get(string terminalId, DateTime? fromDate, DateTime? toDate)
        {
            DateTime fDate = (fromDate == null) ? _settings.FromDate_Telemetry : (DateTime)fromDate;
            DateTime tDate = (toDate == null) ? System.DateTime.Today : (DateTime)toDate;

            if (tDate < fDate)
            {
				tDate = fDate;
            }

            if (fDate >= System.DateTime.Now)
            {
				fDate = System.DateTime.Now;
            }

            if (toDate >= System.DateTime.Now)
            {
                toDate = System.DateTime.Now;
            }

            fDate = DateOnly.FromDateTime(fDate).ToDateTime(TimeOnly.Parse("00:00:00 AM"));
            tDate = DateOnly.FromDateTime(tDate).ToDateTime(TimeOnly.Parse("00:00:00 AM"));
            tDate = tDate.AddDays(1).AddSeconds(-1);

            TerminalMetrics terminalMetrics = new TerminalMetrics();
            TerminalMetricsRecord terminalMetricsRecord;

            List<TelemetryItem> dbTelemetryItems = Helper.Telemetries.Get(_configuration, _logger, _settings, terminalId, fDate, tDate);

            foreach (TelemetryItem telemetryItem in dbTelemetryItems.OrderBy(a => a.TimeStamp))
            {
                terminalMetricsRecord = new TerminalMetricsRecord();

                terminalMetricsRecord.timestamp = telemetryItem.TimeStamp;
                terminalMetricsRecord.TerminalId = telemetryItem.TerminalId;
                terminalMetricsRecord.DownlinkThroughput = telemetryItem.DownlinkThroughput;
                terminalMetricsRecord.UplinkThroughput = telemetryItem.UplinkThroughput;
                terminalMetricsRecord.PingDropRateAvg = telemetryItem.PingDropRateAvg * 100;
                terminalMetricsRecord.PingLatencyMsAvg = telemetryItem.PingLatencyMsAvg;
                terminalMetricsRecord.ObstructionPercentTime = telemetryItem.ObstructionPercentTime;
                terminalMetricsRecord.SignalQuality = telemetryItem.SignalQuality * 100;

                terminalMetrics.TerminalMetricsList.Add(terminalMetricsRecord);
            }

            if (_telemetryItemList.ContainsKey(terminalId))
			{
				Dictionary<DateTime, TelemetryItem> telemetryItems = _telemetryItemList[terminalId];

                foreach (var telemetryItem in telemetryItems.OrderBy(a => a.Value.TimeStamp))
                {
					if(telemetryItem.Key >= fromDate && telemetryItem.Key <= toDate)
					{
						terminalMetricsRecord = new TerminalMetricsRecord();

						terminalMetricsRecord.timestamp = telemetryItem.Value.TimeStamp;
						terminalMetricsRecord.TerminalId = telemetryItem.Value.TerminalId;
						terminalMetricsRecord.DownlinkThroughput = telemetryItem.Value.DownlinkThroughput;
						terminalMetricsRecord.UplinkThroughput = telemetryItem.Value.UplinkThroughput;
						terminalMetricsRecord.PingDropRateAvg = telemetryItem.Value.PingDropRateAvg * 100;
						terminalMetricsRecord.PingLatencyMsAvg = telemetryItem.Value.PingLatencyMsAvg;
						terminalMetricsRecord.ObstructionPercentTime = telemetryItem.Value.ObstructionPercentTime;
						terminalMetricsRecord.SignalQuality = telemetryItem.Value.SignalQuality * 100;

						terminalMetrics.TerminalMetricsList.Add(terminalMetricsRecord);
                    }
                }
            }

			return terminalMetrics;
        }

		IEnumerator<Dictionary<DateTime, TelemetryItem>> IEnumerable<Dictionary<DateTime, TelemetryItem>>.GetEnumerator()
		{
			return _telemetryItemList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _telemetryItemList.GetEnumerator();
		}

        public Dictionary<string, Dictionary<DateTime, TelemetryItem>> GetTelemetryItemList()
        {
            return _telemetryItemList;
        }
    }
}
