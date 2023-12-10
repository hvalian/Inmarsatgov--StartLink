using IG.ENMS.Models.Starlink.NMS;
using IG.ENMS.Starlink.Data;
using StarlinkModels = IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.StateMachines;
using WebAPIModels = IG.ENMS.Models.Starlink.Out.WebAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace IG.ENMS.Starlink.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardsController : Controller
    {
        private readonly ILogger<UserTerminalsController> _logger;
        private readonly StarlinkSM _starlinkSM;
        readonly IConfiguration _configuration;
        protected readonly IMemoryCache _cache;

        public DashboardsController(ILogger<UserTerminalsController> logger, StarlinkSM starlinkSM, IConfiguration configuration, IMemoryCache cache)
        {
            _logger = logger;
            _starlinkSM = starlinkSM;
            _configuration = configuration;
            _cache = cache;
        }

        [Route("TerminalGridData")]
        [HttpGet()]
        [ResponseCache(Duration = 600)]
        public IActionResult TerminalGridData(string? nodeId, string userName)
        {
            UserTerminals? userTerminals = GetUserTerminals(_logger, userName, nodeId);

            if (userTerminals == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Forbidden - User not having the necessary permissions.");
            }

            List<WebAPIModels.TerminalDetailsModel> terminalList = new List<WebAPIModels.TerminalDetailsModel>();

            WebAPIModels.TerminalDetailsModel terminalObject;
            StarlinkModels.ServiceLine terminalServiceLine;
            StarlinkModels.Account terminalAccount;
            StarlinkModels.Subscription terminalSubscription;

            foreach (StarlinkModels.UserTerminal userTerminal in userTerminals) {
                terminalObject = new WebAPIModels.TerminalDetailsModel();
                terminalObject.UserTerminal.UserTerminalId = userTerminal.UserTerminalId;
                terminalObject.UserTerminal.KitSerialNumber = userTerminal.KitSerialNumber;
                terminalObject.UserTerminal.DishSerialNumber = userTerminal.DishSerialNumber;
                terminalObject.UserTerminal.Active = userTerminal.Active;
                terminalObject.ServiceLine.ServiceLineNumber = userTerminal.ServiceLineNumber;

                //populate service line details
                try {
                    terminalServiceLine = _starlinkSM.ServiceLines.Get(userTerminal.ServiceLineNumber);

                    if (terminalServiceLine is not null && terminalServiceLine.Active) {
                        terminalObject.ServiceLine.NickName = terminalServiceLine.Nickname;
                        terminalObject.ServiceLine.Active = terminalServiceLine.Active;

                        StarlinkModels.Address address = _starlinkSM.Addresses.Get(terminalServiceLine.AddressReferenceId);

                        if (address is not null) {
                            terminalObject.ServiceLine.Address.AddressReferenceId = terminalServiceLine.AddressReferenceId;
                            terminalObject.ServiceLine.Address.FormattedAddress = address.FormattedAddress;
                            terminalObject.ServiceLine.Address.Latitude = address.Latitude;
                            terminalObject.ServiceLine.Address.Longitude = address.Longitude;
                        }

                        //populate account details
                        //if the account number is not valid, then just add the terminal to the list and continue.
                        try
                        {
                            if (string.IsNullOrEmpty(terminalServiceLine.AccountNumber))
                            {
                                _logger.LogWarning("Account Number is null for Service Line {serviceLineNumber} for user terminal {userTerminalId}, Dish Serial Number {dishSerialNumber} in state machine.", userTerminal.ServiceLineNumber, userTerminal.UserTerminalId, userTerminal.DishSerialNumber);
                            }
                            else
                            {
                                terminalAccount = _starlinkSM.Accounts.Get(terminalServiceLine.AccountNumber);

                                terminalObject.Account.AccountNumber = terminalAccount.AccountNumber;
                                terminalObject.Account.AccountName = terminalAccount.AccountName;
                            }
                        } catch (Exception _Ex) {
                            _logger.LogError("Could not find Account {accountNumber} in the state machine for user terminal {userTerminal} ({kitSerialNumber}).  Error:{error}", terminalServiceLine.AccountNumber, userTerminal.UserTerminalId, userTerminal.KitSerialNumber, _Ex.Message);
                        }

                        //populate subscription details
                        try {
                            terminalSubscription = _starlinkSM.Subscriptions.Where(s=>s.ServiceLineNumber==terminalObject.ServiceLine.ServiceLineNumber).First();

                            if (terminalSubscription is not null) {
                                terminalObject.Subscription.SubscriptionReferenceId = terminalSubscription.SubscriptionReferenceId;
                                terminalObject.Subscription.Description = terminalSubscription.Description;
                                terminalObject.Subscription.ProductReferenceId = terminalSubscription.ProductReferenceId;
                                terminalObject.Subscription.StartDate = terminalSubscription.StartDate.ToString("MM/dd/yyyy");
                                terminalObject.Subscription.NormalizedStartDate = terminalSubscription.NormalizedStartDate.ToString("MM/dd/yyyy");
                                terminalObject.Subscription.EndDate = terminalSubscription.EndDate.ToString("MM/dd/yyyy");
                                terminalObject.Subscription.ServiceEndDate = terminalSubscription.EndDate.AddDays(-1).ToString("MM/dd/yyyy");

                                StarlinkModels.Product product = _starlinkSM.Products.Get(terminalSubscription.ProductReferenceId);
                                if (product is not null)
                                    terminalObject.Subscription.ProductName = product.Name;
                            }
                        } catch (Exception _Ex) {
                            _logger.LogError("Could not find Account {accountNumber} in the state machine for user terminal {userTerminal} ({kitSerialNumber}).  Error:{error}", terminalServiceLine.AccountNumber, userTerminal.UserTerminalId, userTerminal.KitSerialNumber, _Ex.Message);
                        }

                        terminalList.Add(terminalObject);
                    } else {
                        _logger.LogWarning("Service Line {serviceLineNumber} could not be found for user terminal {dishSerialNumber} in state machine.", userTerminal.ServiceLineNumber, userTerminal.DishSerialNumber);
                    }
                } catch (Exception _Ex) {
                    _logger.LogError("Could not find Service Line {serviceLine} in the state machine for user terminal {userTerminal} ({kitSerialNumber}.  Error:{error}).", userTerminal.ServiceLineNumber, userTerminal.UserTerminalId, userTerminal.KitSerialNumber, _Ex.Message);
                }
            }

            string retVal = @"{ ""data"": " + JsonConvert.SerializeObject(terminalList) + " }";

            return Ok(retVal);
        }

        [Route("TerminalList")]
        [HttpGet()]
        [ResponseCache(Duration = 600)]
        public IActionResult TerminalList(string userName)
        {
            UserTerminals? userTerminals = GetUserTerminals(_logger, userName, null);

            if (userTerminals == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Forbidden - User not having the necessary permissions.");
            }

            List<WebAPIModels.UserTerminalModel> terminalList = new List<WebAPIModels.UserTerminalModel>();

            foreach (StarlinkModels.UserTerminal userTerminal in userTerminals) {
                WebAPIModels.UserTerminalModel terminalObject = new WebAPIModels.UserTerminalModel();

                terminalObject.UserTerminalId = userTerminal.UserTerminalId;
                terminalObject.KitSerialNumber = userTerminal.KitSerialNumber;
                terminalObject.DishSerialNumber = userTerminal.DishSerialNumber;
                terminalObject.Active = userTerminal.Active;
                terminalObject.ServiceLineNumber = userTerminal.ServiceLineNumber;

                terminalList.Add(terminalObject);
            }

            string retVal = @"{ ""data"": " + JsonConvert.SerializeObject(terminalList) + " }";

            return Ok(retVal);
        }

        [Route("TerminalUsage")]
        [HttpGet()]
        [ResponseCache(Duration = 300)]
        public IActionResult TerminalUsage(string nodeId, string userName)
        {
            StarlinkModels.UserTerminal? userTerminal = null;
            UserTerminals? userTerminals = GetUserTerminals(_logger, userName, nodeId);

            if (userTerminals == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Forbidden - User not having the necessary permissions.");
            }

            userTerminal = userTerminals.FirstOrDefault(x => x.UserTerminalId == nodeId);

            if (userTerminal == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "The server successfully processed the request and is not returning any terminal information.");
            }

            WebAPIModels.TerminalUsage terminalUsage = new WebAPIModels.TerminalUsage();
            WebAPIModels.TerminalUsage.UsageInfo usageInfo;

            Dictionary<DateTime, StarlinkModels.DataUsage> DailyUsages = _starlinkSM.DailyDataUsages.Get(userTerminal.ServiceLineNumber); 
            if (DailyUsages is not null) {
                foreach (var dailyUsage in DailyUsages.OrderBy(a=>a.Value.StartDate)) {
                    usageInfo = new WebAPIModels.TerminalUsage.UsageInfo();

                    usageInfo.TimeStamp = dailyUsage.Value.StartDate.ToShortDateString();
                    switch (dailyUsage.Value.DataBucketId) {
                        case 1:
                            usageInfo.MobileDataGB = dailyUsage.Value.TotalGB;
                            break;
                        case 2:
                            usageInfo.MobilePriorityDataGB = dailyUsage.Value.TotalGB;
                            break;
                    }

                    terminalUsage.DailyUsage.Add(usageInfo);
                }
            }

            Dictionary<DateTime, StarlinkModels.DataUsage> BillingCycleUsages = _starlinkSM.BillingCycleUsages.Get(userTerminal.ServiceLineNumber); 
            if (BillingCycleUsages is not null) {
                foreach (var billingCycleUsage in BillingCycleUsages.OrderBy(a => a.Value.StartDate)) {
                    usageInfo = new WebAPIModels.TerminalUsage.UsageInfo();

                    usageInfo.TimeStamp = billingCycleUsage.Value.StartDate.ToShortDateString();
                    switch (billingCycleUsage.Value.DataBucketId) {
                        case 1:
                            usageInfo.MobileDataGB = billingCycleUsage.Value.TotalGB;
                            break;
                        case 2:
                            usageInfo.MobilePriorityDataGB = billingCycleUsage.Value.TotalGB;
                            break;
                    }

                    terminalUsage.BillingPeriodUsage.Add(usageInfo);
                }

            }

            string retVal = @"{ ""data"": " + JsonConvert.SerializeObject(terminalUsage) + " }";

            return Ok(retVal);
        }

        [Route("TerminalMetrics")]
        [HttpGet()]
        [ResponseCache(Duration = 300)]
        public IActionResult TerminalMetrics(string nodeId, string userName, DateTime? fromDate = null, DateTime? toDate = null)
        {
            StarlinkModels.UserTerminal? userTerminal = null;
            UserTerminals? userTerminals = GetUserTerminals(_logger, userName, nodeId);

            if (userTerminals == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Forbidden - User not having the necessary permissions.");
            }

            userTerminal = userTerminals.FirstOrDefault(x => x.UserTerminalId == nodeId);

            if (userTerminals.Count() == 0 || userTerminal == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "The server successfully processed the request and is not returning any terminal information.");
            }

            WebAPIModels.TerminalMetrics terminalMetrics = _starlinkSM.TelemetryItems.Get(userTerminal.UserTerminalId, fromDate, toDate);

            string retVal = @"{ ""data"": " + JsonConvert.SerializeObject(terminalMetrics) + " }";

            return Ok(retVal);
        }

        private UserTerminals? GetUserTerminals(ILogger<UserTerminalsController> logger, string userName, string? terminalId)
        {
            Common.UserAccountMapService s = new Common.UserAccountMapService(_configuration, _cache);

            List<UserCustomerTerminalMap> userCustomerTerminalMap = s.GetAccountByUser();

            UserCustomerTerminalMap? userConfig = userCustomerTerminalMap
                  .FirstOrDefault(x => String.Equals(x.userName, userName, StringComparison.OrdinalIgnoreCase));

            if (userConfig == null)
            {
                return null;
            }

            bool hasAccessToAll = (!string.IsNullOrEmpty(userConfig.accountName) && userConfig.accountName == "CSSC");
            bool isInternalUser = (userConfig.Internal);

            UserTerminals userTerminals = new UserTerminals(logger);

            if (string.IsNullOrEmpty(terminalId) && (isInternalUser || hasAccessToAll))
            {
                userTerminals = _starlinkSM.UserTerminals;
            }
            else if (string.IsNullOrEmpty(terminalId))
            {
                var UTList = _starlinkSM.UserTerminals
                    .Where(x => userCustomerTerminalMap.Any(y => (y.terminalID == x.UserTerminalId) &&
                        String.Equals(y.userName, userName, StringComparison.OrdinalIgnoreCase)));

                foreach (var ut in UTList)
                {
                    StarlinkModels.UserTerminal userTerminal = new StarlinkModels.UserTerminal()
                    {
                        UserTerminalId = ut.UserTerminalId,
                        KitSerialNumber = ut.KitSerialNumber,
                        DishSerialNumber = ut.DishSerialNumber,
                        ServiceLineNumber = ut.ServiceLineNumber,
                        Active = ut.Active
                    };

                    userTerminals.Add(userTerminal);
                }
            }
            else if (!string.IsNullOrEmpty(terminalId) && isInternalUser)
            {
                userTerminals.Add(_starlinkSM.UserTerminals.Get(terminalId));
            }
            else
            {
                var Filteredterminal = userCustomerTerminalMap
                    .Where(x => (String.Equals(x.terminalID, terminalId, StringComparison.OrdinalIgnoreCase)) &&
                        String.Equals(x.userName, userName, StringComparison.OrdinalIgnoreCase));

                if (Filteredterminal.Count() > 0)
                {
                    userTerminals.Add(_starlinkSM.UserTerminals.Get(terminalId));
                }
            }

            if (userTerminals.Count() == 0)
            {
                return userTerminals;
            }

            var activeTerminals =
                from userTerminal in userTerminals
                join serviceLine in _starlinkSM.ServiceLines
                    on userTerminal.ServiceLineNumber equals serviceLine.ServiceLineNumber
                where serviceLine.Active == true
                select new StarlinkModels.UserTerminal
                {
                    Active = serviceLine.Active,
                    UserTerminalId = userTerminal.UserTerminalId,
                    ServiceLineNumber = serviceLine.ServiceLineNumber,
                    DishSerialNumber = userTerminal.DishSerialNumber,
                    KitSerialNumber = userTerminal.KitSerialNumber
                };

            UserTerminals terminals = new UserTerminals(logger);
            foreach (var activeTerminal in activeTerminals)
            {
                terminals.Add(activeTerminal);
            }

            return terminals;
        }
    }
}
