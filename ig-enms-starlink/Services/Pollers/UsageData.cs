// File Name: UsageData.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using InAPIModels = IG.ENMS.Models.Starlink.In.APIResponses;
using IG.ENMS.Starlink.StateMachines;
using IG.ENMS.Starlink.Data;
using StarlinkModels = IG.ENMS.Starlink.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace IG.ENMS.Starlink.Services.Pollers
{
    public class UsageData : BackgroundService
    {
        readonly int _recordLimit = 100;

        readonly ILogger<UsageData> _logger;
        readonly IConfiguration _configuration;
        readonly StarlinkSM _starlinkSM;

        private DataUsages _dailyDataUsages;
        private DataUsages _billingCycleUsages;

        public UsageData(IConfiguration configuration, ILogger<UsageData> logger, StarlinkSM starlinkSM)
        {
            _logger = logger;
            _configuration = configuration;
            _starlinkSM = starlinkSM;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<IG.ENMS.Starlink.Data.DataUsages> usages = await Helper.Usages.Get(_configuration, _logger, _starlinkSM.Settings);
            _starlinkSM.BillingCycleUsages = usages[0];
            _starlinkSM.DailyDataUsages = usages[1];

            while (!stoppingToken.IsCancellationRequested) {
                if (_starlinkSM.Settings.Suspended_DataPull)
                {
                    await Task.Delay(60000);
                }
                else
                {
                    if (_starlinkSM.PollUsage) {
                        while (_starlinkSM.IsLocked) {
                            _logger.LogDebug("Waiting for lock relase to start polling for usage data.");
                            await Task.Delay(1000);
                        }

                        while (_starlinkSM.ApiToken is null || _starlinkSM.ApiToken == "") {
                            _logger.LogDebug("Waiting for API token to be fetched.");
                            await Task.Delay(1000);
                        }

                        _logger.LogInformation("Beginning polling for usage data.");
                        _starlinkSM.UsagePollInProgress = true;

                        _dailyDataUsages = new DataUsages(_logger);
                        _billingCycleUsages = new DataUsages(_logger);

                        await GetUsage();

                        _starlinkSM.NewBillingCycleUsages = _billingCycleUsages;
                        _starlinkSM.NewDailyDataUsages = _dailyDataUsages;

                        DataUsages inMemoryBillingCycleUsages = _starlinkSM.BillingCycleUsages;
                        DataUsages inMemoryDailyDataUsages = _starlinkSM.DailyDataUsages;

                        await Helper.Subscriptions.Save(_configuration, _logger, _starlinkSM.ServiceLines, _starlinkSM.Subscriptions);
                        await Helper.Usages.Save(_configuration, _logger, _billingCycleUsages, _dailyDataUsages);

                        usages = await Helper.Usages.Sync(_configuration, _logger, _starlinkSM.Settings, _billingCycleUsages, inMemoryBillingCycleUsages, _dailyDataUsages, inMemoryDailyDataUsages);

                        if (usages != null)
                        {
                            _starlinkSM.BillingCycleUsages = usages[0];
                            _starlinkSM.DailyDataUsages = usages[1];
                        }

                        inMemoryBillingCycleUsages = null;
                        inMemoryDailyDataUsages = null;

                        _dailyDataUsages = null;
                        _billingCycleUsages = null;

                        _starlinkSM.Settings.NextCleanup_UsageData = Helper.Utility.GetNextCleanupValue(_starlinkSM.Settings.NextCleanup_UsageData, _starlinkSM.Settings.CleanUpPolicy_UsageData);
                    }
                    else {
                        _logger.LogInformation("PollUsage is set to false.  Skipping the polling cycle.");
                    }

                    _starlinkSM.UsagePollInProgress = false;
                    _logger.LogInformation("Done polling for usage.");

                    await Task.Delay(_configuration.GetValue<int>("AppSettings:PollFrequencies:UsageData") * 1000);
                }
            }
        }

        private async Task GetUsage()
        {
            int recordLimit = _recordLimit;
            int totalServiceLineCount = 0;

            StarlinkModels.ServicePlan servicePlan;
            StarlinkModels.DataUsage dataUsage;

            _logger.LogInformation("Getting Usage.");

            foreach (StarlinkModels.ServiceLine serviceLine in _starlinkSM.ServiceLines) {
                _logger.LogInformation("Getting usage for account {accountNumber}, service line {serviceLine}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber);

                string response = await GetAPIResponse("account/" + serviceLine.AccountNumber + "/service-lines/" + serviceLine.ServiceLineNumber + "/billing-cycle/all");
                if (response.Trim().Length > 0) {
                    try {
                        InAPIModels.BillingCycleModel billingCycleLineItems = JsonConvert.DeserializeObject<InAPIModels.BillingCycleModel>(response);

                        if (billingCycleLineItems is null) {
                            _logger.LogWarning("Billing Cycle data is null for account {accountNumber}, service line {serviceLine}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber);
                            continue;
                        }

                        _logger.LogInformation("Extracting Service Plan data for account {accountNumber}, service line {serviceLine}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber);

                        if (billingCycleLineItems.Content.ServicePlan is null) {
                            _logger.LogWarning("Service Plan data is null for account {accountNumber}, service line {serviceLine}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber);
                        } else {
                            //add the service place to the service line
                            servicePlan = new StarlinkModels.ServicePlan();

                            if (billingCycleLineItems.Content.ServicePlan.ActiveFrom is not null) {
                                servicePlan.ActiveFrom = (DateTime)billingCycleLineItems.Content.ServicePlan.ActiveFrom;
                            }

                            servicePlan.IsMobilePlan = billingCycleLineItems.Content.ServicePlan.IsMobilePlan;
                            servicePlan.IsoCurrencyCode = billingCycleLineItems.Content.ServicePlan.IsoCurrencyCode;
                            servicePlan.IsOptedIntoOverage = billingCycleLineItems.Content.ServicePlan.IsOptedIntoOverage;
                            servicePlan.OverageDescription = billingCycleLineItems.Content.ServicePlan.OverageDescription;
                            if (billingCycleLineItems.Content.ServicePlan.OverageLineDeactivatedDate is not null) {
                                servicePlan.overageLineDeactivatedDate = (DateTime)billingCycleLineItems.Content.ServicePlan.OverageLineDeactivatedDate;
                            }

                            servicePlan.OverageName = billingCycleLineItems.Content.ServicePlan.OverageName;
                            if (billingCycleLineItems.Content.ServicePlan.OverageLine is not null) {
                                servicePlan.UsageLimitGB = billingCycleLineItems.Content.ServicePlan.OverageLine.UsageLimitGB;
                                servicePlan.OverageAmountGB = billingCycleLineItems.Content.ServicePlan.OverageLine.OverageAmountGB;
                                servicePlan.PricePerGB = billingCycleLineItems.Content.ServicePlan.OverageLine.PricePerGB;
                                servicePlan.OveragePrice =  billingCycleLineItems.Content.ServicePlan.OverageLine.OveragePrice;
                            }

                            serviceLine.ServicePlan = servicePlan;
                        }

                        _logger.LogInformation("Extracting Billing Cycle usage data for account {accountNumber}, service line {serviceLine}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber);

                        if (billingCycleLineItems.Content.BillingCycles is null) {
                            _logger.LogWarning("Billing Cycle usage data is null for account {accountNumber}, service line {serviceLine}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber);
                        } else {
                            foreach (InAPIModels.BillingCycle billingCycle in billingCycleLineItems.Content.BillingCycles) {
                                foreach (InAPIModels.DataUsage inDataUsage in billingCycle.DataUsage) {
                                    _logger.LogInformation("Extracting Billing Cycle usage data for account {accountNumber}, service line {serviceLine}.  Billing cycle starting {startDate}, ending {endDate}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber, billingCycle.StartDate, billingCycle.EndDate);

                                    dataUsage = new StarlinkModels.DataUsage();
                                    dataUsage.ServiceLineNumber = serviceLine.ServiceLineNumber;
                                    dataUsage.StartDate = (DateTime)billingCycle.StartDate;
                                    dataUsage.EndDate = (DateTime)billingCycle.EndDate;
                                    dataUsage.DataBucketId = inDataUsage.DataBucket;
                                    dataUsage.TotalGB = inDataUsage.TotalGB;

                                    _billingCycleUsages.Add(dataUsage);

                                }
                                if (billingCycle.DailyDataUsages is null) {
                                    _logger.LogWarning("Daily usage data is null for account {accountNumber}, service line {serviceLine}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber);
                                } else {
                                    foreach (InAPIModels.DailyDataUsage dailyDataUsage in billingCycle.DailyDataUsages) {
                                        _logger.LogInformation("Extracting daily usage data for account {accountNumber}, service line {serviceLine}.  Billing cycle for {date}.", serviceLine.AccountNumber, serviceLine.ServiceLineNumber, dailyDataUsage.Date);

                                        foreach (InAPIModels.DataUsageBin dataUsageBin in dailyDataUsage.DataUsageBins) {
                                            dataUsage = new StarlinkModels.DataUsage();
                                            dataUsage.ServiceLineNumber = serviceLine.ServiceLineNumber;
                                            dataUsage.StartDate = (DateTime)dailyDataUsage.Date;
                                            dataUsage.EndDate = (DateTime)dailyDataUsage.Date;
                                            dataUsage.DataBucketId = dataUsageBin.DataBucket;
                                            dataUsage.TotalGB = dataUsageBin.TotalGB;

                                            _dailyDataUsages.Add(dataUsage);
                                        }
                                    }
                                }
                            }
                        }
                    } catch (Exception ex) {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            _logger.LogInformation("Fetched {count} service line records for all accounts.", totalServiceLineCount);
        }

        private async Task<string> GetAPIResponse(string endPoint)
        {
            string retVal = "";
            string baseUrl = _configuration.GetValue<string>("AppSettings:SourceUrls:BaseUrl");

            if (baseUrl.Trim().Length == 0) { 
                _logger.LogCritical("Base URL in the appsettings file is blank.");
                return "";
            }

            if (_starlinkSM.ApiToken == "") {
                _logger.LogCritical("Invalid API Token.");
                return "";
            }

            try {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseUrl + "/");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _starlinkSM.ApiToken);
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(endPoint);
                if (httpResponseMessage.IsSuccessStatusCode) {
                    retVal = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                return retVal;
            } catch (Exception _Ex) {
                _logger.LogError("Error during HTTP request [{request}] using API token {token}.  Error:{error}", endPoint, _starlinkSM.ApiToken, _Ex.Message);
                return string.Empty;
            }
        }
    }
}