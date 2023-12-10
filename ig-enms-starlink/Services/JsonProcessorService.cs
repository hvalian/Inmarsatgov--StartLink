using IG.ENMS.Starlink.Data;
using IG.ENMS.Starlink.Services.Pollers;
using IG.ENMS.Starlink.StateMachines;
using Newtonsoft.Json;
using System.Configuration;
using IG.ENMS.Starlink.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Helper;

namespace IG.ENMS.Starlink.Services
{
    public class JsonProcessorService : BackgroundService
    {
        readonly IConfiguration _configuration;
        readonly StarlinkSM _starlinkSM;

        string? FilePath { get; set; }
        List<string>? TelemetryFiles { get; set; }
        List<string>? UsageBillingFiles { get; set; }
        List<string>? UsageDailyFiles { get; set; }

        public JsonProcessorService(IConfiguration configuration, StarlinkSM starlinkSM)
        {
            _configuration = configuration;
            _starlinkSM = starlinkSM;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_starlinkSM.Settings.Suspended_JsonProcessor)
                {
                    await Task.Delay(60000);
                    continue;
                }

                this.FilePath = _configuration.GetValue<string>("AppSettings:StateMachine:PersistenceLocation");
                this.TelemetryFiles = new List<string>();
                this.UsageBillingFiles = new List<string>();
                this.UsageDailyFiles = new List<string>();

                await PerpareLists();

                await ProcessTelemetryFiles();
                await ProcessUsageFiles(UsageBillingFiles, Usage.Billing);
                await ProcessUsageFiles(UsageDailyFiles, Usage.Daily);
            }
        }

        private async Task MoveFile(string filename, bool suported, bool successful)
        {
            string subDirName = (suported ? (successful ? @"\Processed" : @"\Errors") : @"\Archive");
            try
            {
                string jsonData = await File.ReadAllTextAsync(filename);

                if (!Directory.Exists(this.FilePath + @"\" + subDirName))
                {
                    DirectoryInfo di = Directory.CreateDirectory(this.FilePath + @"\" + subDirName);
                }

                var fileInfo = new FileInfo(filename);

                File.Move(fileInfo.DirectoryName + @"\" + fileInfo.Name, this.FilePath + @"\" + subDirName + @"\" + fileInfo.Name, true);
            }
            catch (Exception _Ex)
            {
            }

            await Task.Delay(1);
        }

        private async Task PerpareLists()
        {
            if (!Directory.Exists(this.FilePath))
            {
                return;
            }

            foreach (string file in Directory.EnumerateFiles(this.FilePath, "*.*"))
            {
                string filenameWithoutPath = Path.GetFileName(file);
                if (filenameWithoutPath.ToLower().StartsWith("telemetry") && filenameWithoutPath.EndsWith(".json"))
                {
                    this.TelemetryFiles.Add(file);
                }
                else if (filenameWithoutPath.ToLower().StartsWith("usage-billingcycle") && filenameWithoutPath.EndsWith(".json"))
                {
                    this.UsageBillingFiles.Add(file);
                }
                else if (filenameWithoutPath.ToLower().StartsWith("usage-dailydata") && filenameWithoutPath.EndsWith(".json"))
                {
                    this.UsageDailyFiles.Add(file);
                }
                else
                {
                   await MoveFile(file, false, false);
                }
            }

            this.TelemetryFiles = this.TelemetryFiles.OrderBy(q => q).ToList();
            this.UsageBillingFiles = this.UsageBillingFiles.OrderBy(q => q).ToList();
            this.UsageDailyFiles = this.UsageDailyFiles.OrderBy(q => q).ToList();

            await Task.Delay(1);

            return;
        }

        private async Task ProcessTelemetryFiles()
        {
            foreach (string file in TelemetryFiles)
            {
                List<TelemetryItem> telemetryItems = new List<TelemetryItem>();

                bool successful = false;

                try
                {
                    string jsonData = await File.ReadAllTextAsync(file); 

                    if (jsonData is not null && jsonData != string.Empty)
                    {
                        JArray responseData = JArray.Parse(jsonData);
                        foreach (JObject item in responseData)
                        {
                            try
                            {
                                string terminalId = item.GetValue("Key").ToString();
                                string json = item.GetValue("Value").ToString();
                                if (!string.IsNullOrWhiteSpace(json))
                                {
                                    Dictionary<DateTime, TelemetryItem> jsonDictionary = JsonConvert.DeserializeObject<Dictionary<DateTime, TelemetryItem>>(json);

                                    var objectDictionary = jsonDictionary.ToDictionary(x => x.Key, x => x.Value);

                                    foreach (KeyValuePair<DateTime, TelemetryItem> entry in objectDictionary)
                                    {
                                        TelemetryItem telemetryItem = entry.Value;
                                        telemetryItems.Add(telemetryItem);
                                    }
                                }
                            }
                            catch (Exception _Ex)
                            {
                            }
                        }

                        if (telemetryItems.Count > 0) 
                        {
                            await Helper.Telemetries.Save(_configuration, file, telemetryItems);
                        }

                        successful = true;
                    }
                }
                catch (Exception _Ex)
                {
                }

                await MoveFile(file, true, successful);
            }

            await Task.Delay(1);

            return;
        }

        private async Task ProcessUsageFiles(List<string> usageFiles, Usage usage)
        {
            foreach (string file in usageFiles)
            {
                List<DataUsage> usageDataItems = new List<DataUsage>();

                bool successful = false;

                try
                {
                    string jsonData = await File.ReadAllTextAsync(file);

                    if (jsonData is not null && jsonData != string.Empty)
                    {
                        JArray responseData = JArray.Parse(jsonData);
                        foreach (JObject item in responseData)
                        {
                            try
                            {
                                string terminalId = item.GetValue("Key").ToString();
                                string json = item.GetValue("Value").ToString();
                                if (!string.IsNullOrWhiteSpace(json))
                                {
                                    Dictionary<string, DataUsage> jsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, DataUsage>>(json);

                                    var objectDictionary = jsonDictionary.ToDictionary(x => x.Key, x => x.Value);

                                    foreach (KeyValuePair<string, DataUsage> entry in objectDictionary)
                                    {
                                        DataUsage dataUsage = entry.Value;
                                        usageDataItems.Add(dataUsage);
                                    }
                                }
                            }
                            catch (Exception _Ex)
                            {
                            }
                        }

                        if (usageDataItems.Count > 0)
                        {
                            await Helper.Usages.Save(_configuration, file, usageDataItems, usage);
                        }

                        successful = true;
                    }
                }
                catch (Exception _Ex)
                {
                }

                await MoveFile(file, true, successful);
            }

            await Task.Delay(1);

            return;
        }
    }
}

