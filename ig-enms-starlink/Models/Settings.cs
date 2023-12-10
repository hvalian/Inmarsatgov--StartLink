using System.Configuration;
using System.Runtime;

namespace IG.ENMS.Starlink.Models
{
    public class Settings
    {
        private double GetCleanUpPolicy_TelemetryData(IConfiguration configuration)
        {
            double? value = configuration.GetValue<double>("CleanUpPolicy:TelemetryData");
            if (value == null || value == 0)
            {
                value = 12;
            }

            return (double)value;
        }

        private double GetCleanUpPolicy_UsageData(IConfiguration configuration)
        {
            double? value = configuration.GetValue<double>("CleanUpPolicy:UsageData");
            if (value == null || value == 0)
            {
                value = 12;
            }

            return (double)value;
        }

        private double GetRetentionPolicy_TelemetryData(IConfiguration configuration)
        {
            double? value = configuration.GetValue<double>("RetentionPolicy:TelemetryData");
            if (value == null || value == 0)
            {
                value = 30;
            }

            return (double)value;
        }

        private double GetRetentionPolicy_UsageDataBilling(IConfiguration configuration)
        {
            double? value = configuration.GetValue<double>("RetentionPolicy:UsageData_Billing");
            if (value == null || value == 0)
            {
                value = 124;
            }

            return (double)value;
        }

        private double GetRetentionPolicy_UsageDataDaily(IConfiguration configuration)
        {
            double? value = configuration.GetValue<double>("RetentionPolicy:UsageData_Daily");
            if (value == null || value == 0)
            {
                value = 62;
            }

            return (double)value;
        }

        private bool GetSuspendPolicy_DataPull(IConfiguration configuration)
        {
            string? value = configuration.GetValue<string>("SuspensionPolicy:DataPull");
            return (value != null && (value.ToUpper() == "TRUE" || value == "1")) ? true : false;
        }

        private bool GetSuspendPolicy_JsonProcessor(IConfiguration configuration)
        {
            string? value = configuration.GetValue<string>("SuspensionPolicy:JsonProcessor");
            return (value != null && (value.ToUpper() == "TRUE" || value == "1")) ? true : false;
        }

        private bool GetSuspendPolicy_SaveToDisk(IConfiguration configuration)
        {
            string? value = configuration.GetValue<string>("SuspensionPolicy:SaveToDisk");
            return (value != null && (value.ToUpper() == "TRUE" || value == "1")) ? true : false;
        }

        public Settings(IConfiguration configuration)
        {
            CleanUpPolicy_TelemetryData = GetCleanUpPolicy_TelemetryData(configuration);
            CleanUpPolicy_UsageData = GetCleanUpPolicy_UsageData(configuration);
            NextCleanup_TelemetryData = System.DateTime.Now.AddHours(CleanUpPolicy_TelemetryData);
            NextCleanup_UsageData = System.DateTime.Now.AddHours(CleanUpPolicy_UsageData);
            RetentionPolicy_TelemetryData = GetRetentionPolicy_TelemetryData(configuration);
            RetentionPolicy_UsageDataBilling = GetRetentionPolicy_UsageDataBilling(configuration);
            RetentionPolicy_UsageDataDaily = GetRetentionPolicy_UsageDataDaily(configuration);
            Suspended_DataPull = GetSuspendPolicy_DataPull(configuration);
            Suspended_JsonProcessor = GetSuspendPolicy_JsonProcessor(configuration);
            Suspended_SaveToDisk = GetSuspendPolicy_SaveToDisk(configuration);
        }

        public double CleanUpPolicy_TelemetryData { get; set; }
        public double CleanUpPolicy_UsageData { get; set; }
        public DateTime FromDate_BillingUsage { get => System.DateTime.Today.AddDays((double) -RetentionPolicy_UsageDataBilling); }
        public DateTime FromDate_DailyUsage { get => System.DateTime.Today.AddDays((double) -RetentionPolicy_UsageDataDaily); }
        public DateTime FromDate_Telemetry { get => System.DateTime.Today.AddDays((double) - RetentionPolicy_TelemetryData); }
        public DateTime NextCleanup_TelemetryData { get; set; }
        public DateTime NextCleanup_UsageData { get; set; }
        public double RetentionPolicy_TelemetryData { get; set; }
        public double RetentionPolicy_UsageDataBilling { get; set; }
        public double RetentionPolicy_UsageDataDaily { get; set; }
        public bool Suspended_DataPull { get; set; }
        public bool Suspended_JsonProcessor { get; set; }
        public bool Suspended_SaveToDisk { get; set; }
    }
}

