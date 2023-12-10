// File Name: TelemetryData.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System;
using IG.ENMS.Models.Starlink.In.APIResponses;
using System.Net.Http.Headers;
using IG.ENMS.Starlink.StateMachines;
using IG.ENMS.Starlink.Data;
using StarlinkModels = IG.ENMS.Starlink.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IG.ENMS.Starlink.Helper;

namespace IG.ENMS.Starlink.Services.Pollers
{
    public class TelemetryData : BackgroundService
    {
        readonly ILogger<TelemetryData> _logger;
        readonly IConfiguration _configuration;
        readonly StarlinkSM _starlinkSM;

        private TelemetryItems _telemetryItems;

        public TelemetryData(IConfiguration configuration, ILogger<TelemetryData> logger, StarlinkSM starlinkSM)
        {
            _logger = logger;
            _configuration = configuration;
            _starlinkSM = starlinkSM;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IG.ENMS.Starlink.Data.TelemetryItems telemetryItems = await Helper.Telemetries.Get(_configuration, _logger, _starlinkSM.Settings);
            _starlinkSM.TelemetryItems = telemetryItems;
            telemetryItems = null;

            while (!stoppingToken.IsCancellationRequested) {
                if (_starlinkSM.Settings.Suspended_DataPull)
                {
                    await Task.Delay(60000);
                }
                else
                {
                    if (_starlinkSM.PollTelemetry) {
                        while (_starlinkSM.IsLocked) {
                            _logger.LogDebug("Awaiting Accounts poll to complete.");
                            await Task.Delay(1000);
                        }

                        _logger.LogInformation("Beginning polling for telemetry.");
                        _starlinkSM.TelemetryPollInProgress = true;

                        TelemetryItems? existingTelemetryItems = _starlinkSM.TelemetryItems;

                        _telemetryItems = new TelemetryItems(_configuration, _logger, _starlinkSM.Settings);

                        await GetTelemetry();

                        _starlinkSM.NewTelemetryItems = _telemetryItems;

                        telemetryItems = await Helper.Telemetries.Save(_configuration, _logger, _starlinkSM.Settings, _telemetryItems, existingTelemetryItems);

                        existingTelemetryItems = null;
                        _starlinkSM.TelemetryItems = telemetryItems;

                        _starlinkSM.Settings.NextCleanup_TelemetryData = Helper.Utility.GetNextCleanupValue(_starlinkSM.Settings.NextCleanup_TelemetryData, _starlinkSM.Settings.CleanUpPolicy_TelemetryData);
                    }
                    else
                    {
                        _logger.LogInformation("PollTelemetry is set to false.  Skipping the polling cycle.");
                    }

                    _starlinkSM.TelemetryPollInProgress = false;
                    _logger.LogInformation("Done polling for telemetry.");

                    await Task.Delay(_configuration.GetValue<int>("AppSettings:PollFrequencies:TelemetryData") * 1000);
                }
            }
        }

        private async Task GetTelemetry()
        {
            bool fetch;
            string telemetryData = string.Empty;
            JToken? columnNames = null;
            JToken? alertLookup = null;
            Dictionary<string, string> alertList = new Dictionary<string, string>();

            foreach (StarlinkModels.Account account in _starlinkSM.Accounts) {
                fetch = true;
                while (fetch) {
                    telemetryData = await GetTelemetry(account.AccountNumber);

                    if (telemetryData is not null && telemetryData != string.Empty) {
                        JObject responseData = JObject.Parse(telemetryData);
                        JToken? data = responseData.SelectToken("data.values");

                        //Extract the column names and alert descriptions only the first time.
                        if (columnNames is null)
                            try {
                                columnNames = responseData.SelectToken("data.columnNamesByDeviceType.u");
                            } catch (Exception _Ex) {
                                _logger.LogError("Error parsing column names from telemetry response.  Error:{error}", _Ex.Message);

                            }

                        if (alertLookup is null) {
                            try {
                                alertLookup = responseData.SelectToken("metadata.enums.AlertsByDeviceType.u");
                                if (alertLookup is not null)
                                {
                                    foreach (JProperty alertItem in alertLookup)
                                    {
                                        alertList.Add(alertItem.Name.ToString(), alertItem.Value.ToString());
                                    }
                                }
                            } catch (Exception _Ex) {
                                _logger.LogError("Error parsing alert descriptions from telemetry response.  Error:{error}", _Ex.Message);
                            }
                        }

                        StarlinkModels.TelemetryItem telemetry;
                        string alertDesc = string.Empty;

                        try {
                            foreach (JArray jValue in data) {
                                try {
                                    telemetry = new StarlinkModels.TelemetryItem();
                                    telemetry.DeviceType = ((JValue)jValue[0]).Value.ToString();
                                    telemetry.UtcTimestampNs = Convert.ToInt64(((JValue)jValue[1]).Value.ToString());
                                    telemetry.DeviceId = ((JValue)jValue[2]).Value.ToString();
                                    telemetry.DownlinkThroughput = Convert.ToDouble(((JValue)jValue[3]).Value.ToString());
                                    telemetry.UplinkThroughput = Convert.ToDouble(((JValue)jValue[4]).Value.ToString());
                                    telemetry.PingDropRateAvg = Convert.ToDouble(((JValue)jValue[5]).Value.ToString());
                                    telemetry.PingLatencyMsAvg = Convert.ToInt32(((JValue)jValue[6]).Value.ToString());
                                    telemetry.ObstructionPercentTime = Convert.ToDouble(((JValue)jValue[7]).Value.ToString());
                                    telemetry.Uptime = Convert.ToInt32(((JValue)jValue[8]).Value.ToString());
                                    telemetry.SignalQuality = Convert.ToDouble(((JValue)jValue[9]).Value.ToString());

                                    if (jValue[10] is not null)
                                        foreach (JValue alertItem in jValue[10]) {
                                            if (alertList.ContainsKey(alertItem.ToString()))
                                                alertDesc = alertList[alertItem.ToString()];
                                            else
                                                alertDesc = "No description found for alert code " + alertItem.ToString();

                                            telemetry.Alerts.Add("[" + alertItem.ToString() + "] " + alertDesc);
                                        }

                                    _telemetryItems.Add(telemetry);
                                } 
                                catch (Exception _Ex) {
                                    _logger.LogError("Error processing telemetry record for account {accountNumber}. [{telemetryRecord}].  Error:{error}", account.AccountNumber, jValue.ToString(), _Ex.Message);
                                }
                            }
                            _logger.LogInformation("Fetched {dataRecords} data records for {accountNumber}.", (data is not null) ? data.Count() : 0, account.AccountNumber);
                        } catch (Exception _Ex) {
                            _logger.LogError("Error processing telemetry records for account {accountNumber}.  Error:{error}", account.AccountNumber, _Ex.Message);
                        }

                        if (data.Count() == 1000)
                            fetch = true;
                        else
                            fetch = false;
                    } else {
                        fetch = false;
                    }
                }
            }
        }

        private async Task<string> GetTelemetry(string AccountNumber)
        {
            _logger.LogInformation("Getting Telemetry for account {account}", AccountNumber);

            string telemetryUrl = _configuration.GetValue<string>("AppSettings:SourceUrls:TelemetryUrl");

            if (telemetryUrl.Trim().Length == 0) {
                _logger.LogCritical("Telemetry URL in the appsettings file is blank.");
                return string.Empty;
            }

            if (_starlinkSM.ApiToken == "") {
                _logger.LogCritical("Invalid API Token.");
                return string.Empty;
            }

            try {
                var bodyContent = new {
                    accountNumber = AccountNumber,
                    batchSize = 1000,
                    maxLingerMs = 500
                };

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _starlinkSM.ApiToken);

                string bodyJson = JsonConvert.SerializeObject(bodyContent);
                var content = new StringContent(bodyJson, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(telemetryUrl, content);
                if (httpResponseMessage.IsSuccessStatusCode) {
                    string response = await httpResponseMessage.Content.ReadAsStringAsync();
                    return response;
                } else {
                    _logger.LogError("Error during HTTP request [{request}] using AccountNumber {accountNumber}.  HttpResponseMessage:{error}", telemetryUrl, AccountNumber, httpResponseMessage);
                    return string.Empty;
                }
            } catch (Exception _Ex) {
                _logger.LogError("Error during HTTP request [{request}] using API token {token}.  Error:{error}", telemetryUrl, _starlinkSM.ApiToken, _Ex.Message);
                return "";
            }
        }
    }
}