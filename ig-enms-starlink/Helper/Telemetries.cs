using IG.ENMS.Starlink.Data;
using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Services.Pollers;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Helper
{
    public class Telemetries
    {
        private static int Count(TelemetryItems telemetryItems)
        {
            int count = 0;

            foreach (var termainalTelemetryItems in telemetryItems)
            {
                count += termainalTelemetryItems.Values.Count();
            }

            return count;
        }

        public static async Task<IG.ENMS.Starlink.Data.TelemetryItems> Get(IConfiguration configuration, ILogger<TelemetryData> logger, Settings settings)
        {
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);
            IG.ENMS.Starlink.Data.TelemetryItems telemetryItems = new IG.ENMS.Starlink.Data.TelemetryItems(configuration, logger, settings);

            if (dbContext == null)
            {
                logger.LogError("Error processing telemetries. Can't create instance of IgenmsContext.");
                return telemetryItems;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            dbContext.Database.SetCommandTimeout(1800);

            var telemetries = dbContext.GetTelemetries_Result
                .FromSqlInterpolated($"[starlink].[GetTelemetries] {settings.FromDate_Telemetry}")
                .ToArray();

            foreach (var telemetry in telemetries)
            {
                IG.ENMS.Starlink.Models.TelemetryItem telemetryItem = new IG.ENMS.Starlink.Models.TelemetryItem();
                telemetryItem.TerminalId = telemetry.TerminalId;
                telemetryItem.DownlinkThroughput = telemetry.DownlinkThroughput;
                telemetryItem.UplinkThroughput = telemetry.UplinkThroughput;
                telemetryItem.PingDropRateAvg = telemetry.PingDropRateAvg;
                telemetryItem.PingLatencyMsAvg = telemetry.PingLatencyMsAvg;
                telemetryItem.ObstructionPercentTime = telemetry.ObstructionPercentTime;
                telemetryItem.Uptime = telemetry.Uptime;
                telemetryItem.SignalQuality = telemetry.SignalQuality;
                telemetryItem.TimeStamp = telemetry.TimeStamp;
                telemetryItems.Add(telemetryItem);
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Telemetry, "FetchData", (int)watch.ElapsedMilliseconds, telemetries.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return telemetryItems;
        }

        public static List<TelemetryItem> Get(IConfiguration configuration, ILogger logger, Settings settings, string TerminalId, DateTime fromDate, DateTime toDate)
        {
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);
            List<TelemetryItem> telemetryItems = new List<TelemetryItem>();

            if (dbContext == null)
            {
                logger.LogError("Error processing telemetries. Can't create instance of IgenmsContext.");
                return telemetryItems;
            }

            var telemetries = dbContext.GetTelemetries_Result
                .FromSqlInterpolated($"[starlink].[GetTelemetriesByTerminalId] {TerminalId}, {fromDate}, {toDate}")
                .ToArray();

            foreach (var telemetry in telemetries)
            {
                IG.ENMS.Starlink.Models.TelemetryItem telemetryItem = new IG.ENMS.Starlink.Models.TelemetryItem();
                telemetryItem.TerminalId = telemetry.TerminalId;
                telemetryItem.DownlinkThroughput = telemetry.DownlinkThroughput;
                telemetryItem.UplinkThroughput = telemetry.UplinkThroughput;
                telemetryItem.PingDropRateAvg = telemetry.PingDropRateAvg;
                telemetryItem.PingLatencyMsAvg = telemetry.PingLatencyMsAvg;
                telemetryItem.ObstructionPercentTime = telemetry.ObstructionPercentTime;
                telemetryItem.Uptime = telemetry.Uptime;
                telemetryItem.SignalQuality = telemetry.SignalQuality;
                telemetryItem.TimeStamp = telemetry.TimeStamp;
                telemetryItems.Add(telemetryItem);
            }

            dbContext.Database.CloseConnection();

            return telemetryItems;
        }

        private static string GetMetricValue(string metricKey, TelemetryItem telemetryItem)
        {
            switch (metricKey)
            {
                case "DownlinkThroughput":
                    return telemetryItem.DownlinkThroughput.ToString();
                case "ObstructionPercentTime":
                    return telemetryItem.ObstructionPercentTime.ToString();
                case "PingDropRateAvg":
                    return telemetryItem.PingDropRateAvg.ToString();
                case "PingLatencyMsAvg":
                    return telemetryItem.PingLatencyMsAvg.ToString();
                case "SignalQuality":
                    return telemetryItem.SignalQuality.ToString();
                case "UplinkThroughput":
                    return telemetryItem.UplinkThroughput.ToString();
                case "Uptime":
                    return telemetryItem.Uptime.ToString();
                default:
                    return "";
            }
        }

        public static async Task<TelemetryItems> Save(IConfiguration configuration, ILogger<TelemetryData> logger, Settings settings, TelemetryItems telemetryItems, TelemetryItems existingTelemetryItems)
        {
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing telemetries. Can't create instance of IgenmsContext.");
                return existingTelemetryItems;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            dbContext.Database.SetCommandTimeout(1800);

            List<TbNodeMetricValue> datas = new List<TbNodeMetricValue>();
            List<TbNode> nodes = await Task.Run(() => dbContext.TbNodes
                .Include(x => x.TbNodeMetrics)
                .ToList());

            string metricKey = "";
            int nodeId = 0;

            foreach (var item in telemetryItems)
            {
                foreach (var dataItem in item)
                {
                    TelemetryItem telemetryItem = dataItem.Value;
                    try
                    {
                        TbNode? node = nodes.FirstOrDefault(x => x.Name == telemetryItem.TerminalId);

                        if (node != null)
                        {
                            foreach (TbNodeMetric metric in node.TbNodeMetrics)
                            {
                                metricKey = metric.MetricKey;
                                nodeId = node.Id;

                                TbNodeMetricValue tbNodeMetricValue = new TbNodeMetricValue();
                                tbNodeMetricValue.NodeId = node.Id;
                                tbNodeMetricValue.NodeMetricId = metric.Id;
                                tbNodeMetricValue.NodeTypeId = node.NodeTypeId;
                                tbNodeMetricValue.MetricKey = metric.MetricKey;
                                tbNodeMetricValue.MetricValueType = metric.MetricValueType;
                                tbNodeMetricValue.Timestamp = telemetryItem.TimeStamp;
                                tbNodeMetricValue.Value = GetMetricValue(metric.MetricKey, telemetryItem);
                                tbNodeMetricValue.Metricvalue1 = null;
                                tbNodeMetricValue.Metricvalue2 = null;
                                tbNodeMetricValue.Metricvalue3 = null;
                                tbNodeMetricValue.EpochTimestamp = telemetryItem.UtcTimestampNs;
                                tbNodeMetricValue.LastPolledTimestamp = telemetryItem.UtcTimestampNs;
                                tbNodeMetricValue.IsbackFilled = false;
                                datas.Add(tbNodeMetricValue);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "Save", ex, "Error processing telemetries.  NodeId: " + nodeId.ToString() + " MetricKey: " + metricKey);
                    }
                }
            }

            try
            {
                dbContext.BulkInsertOrUpdate(datas);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "Save", ex, "BulkInsertOrUpdate");
            }
            catch (Exception ex)
            {
                Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "Save", ex, "BulkInsertOrUpdate");
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Telemetry, "Save", (int)watch.ElapsedMilliseconds, datas.Count);

            watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var item in telemetryItems)
            {
                foreach (var dataItem in item)
                {
                    TelemetryItem telemetryItemData = dataItem.Value;

                    if (existingTelemetryItems.GetTelemetryItemList().ContainsKey(telemetryItemData.TerminalId))
                    {
                        if (existingTelemetryItems.GetTelemetryItemList()[telemetryItemData.TerminalId].ContainsKey(telemetryItemData.TimeStamp))
                        {
                            existingTelemetryItems.GetTelemetryItemList()[telemetryItemData.TerminalId][telemetryItemData.TimeStamp] = telemetryItemData;
                        }
                        else
                        {
                            existingTelemetryItems.GetTelemetryItemList()[telemetryItemData.TerminalId].Add(telemetryItemData.TimeStamp, telemetryItemData);
                        }
                    }
                    else
                    {
                        existingTelemetryItems.GetTelemetryItemList().Add(telemetryItemData.TerminalId, new Dictionary<DateTime, TelemetryItem>());
                        existingTelemetryItems.GetTelemetryItemList()[telemetryItemData.TerminalId].Add(telemetryItemData.TimeStamp, telemetryItemData);
                    }
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Telemetry, "Count", (int)watch.ElapsedMilliseconds, Count(existingTelemetryItems));

            if (settings.NextCleanup_TelemetryData <= System.DateTime.Now)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();

                int count = 0;

                foreach (var item in existingTelemetryItems)
                {
                    int xcount = item.Count();
                    foreach (var s in item.Where(kv => (System.DateTime.Now - kv.Key).Days > settings.RetentionPolicy_TelemetryData).ToList())
                    {
                        item.Remove(s.Key);
                    }
                    xcount -= item.Count();
                    count += xcount;
                }

                watch.Stop();

                Helper.Utility.WriteToLogActivity(dbContext, Category.Telemetry, "CleanUP", (int)watch.ElapsedMilliseconds, count);
            }

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return existingTelemetryItems;
        }

        public static async Task Save(IConfiguration configuration,string fileName, List<TelemetryItem> telemetryItems)
        {
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            dbContext.Database.SetCommandTimeout(1800);

            List<TbNodeMetricValue> datas = new List<TbNodeMetricValue>();
            List<TbNode> nodes = await Task.Run(() => dbContext.TbNodes
                    .Include(x => x.TbNodeMetrics)
                    .ToList());

            string metricKey = "";
            int nodeId = 0;

            foreach (TelemetryItem telemetryItem in telemetryItems)
            {
                try
                {
                    TbNode? node = nodes.FirstOrDefault(x => x.Name == telemetryItem.TerminalId);

                    if (node != null)
                    {
                        foreach (TbNodeMetric metric in node.TbNodeMetrics)
                        {
                            metricKey = metric.MetricKey;
                            nodeId = node.Id;

                            TbNodeMetricValue tbNodeMetricValue = new TbNodeMetricValue();
                            tbNodeMetricValue.NodeId = node.Id;
                            tbNodeMetricValue.NodeMetricId = metric.Id;
                            tbNodeMetricValue.NodeTypeId = node.NodeTypeId;
                            tbNodeMetricValue.MetricKey = metric.MetricKey;
                            tbNodeMetricValue.MetricValueType = metric.MetricValueType;
                            tbNodeMetricValue.Timestamp = telemetryItem.TimeStamp;
                            tbNodeMetricValue.Value = GetMetricValue(metric.MetricKey, telemetryItem);
                            tbNodeMetricValue.Metricvalue1 = null;
                            tbNodeMetricValue.Metricvalue2 = null;
                            tbNodeMetricValue.Metricvalue3 = null;
                            tbNodeMetricValue.EpochTimestamp = telemetryItem.UtcTimestampNs;
                            tbNodeMetricValue.LastPolledTimestamp = telemetryItem.UtcTimestampNs;
                            tbNodeMetricValue.IsbackFilled = false;
                            datas.Add(tbNodeMetricValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "JsaonFile", ex, "Error processing telemetries.  NodeId: " + nodeId.ToString() + " MetricKey: " + metricKey);
                }
            }

            try
            {
                dbContext.BulkInsertOrUpdate(datas);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "JsaonFile", ex, "BulkInsertOrUpdate");
            }
            catch (Exception ex)
            {
                Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "JsaonFile", ex, "BulkInsertOrUpdate");
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Telemetry, "JsaonFile", fileName, (int)watch.ElapsedMilliseconds, datas.Count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return;
        }
    }
}


