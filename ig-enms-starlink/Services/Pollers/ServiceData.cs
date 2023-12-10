// File Name: ServiceData.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System.Globalization;
using IG.ENMS.Models.Starlink.In.APIResponses;
using System.Net.Http.Headers;
using IG.ENMS.Starlink.StateMachines;
using IG.ENMS.Starlink.Data;
using StarlinkModels = IG.ENMS.Starlink.Models;
using Newtonsoft.Json;

namespace IG.ENMS.Starlink.Services.Pollers
{
    public class ServiceData : BackgroundService
    {
        readonly int _recordLimit = 100;

        readonly ILogger<ServiceData> _logger;
        readonly IConfiguration _configuration;
        readonly StarlinkSM _starlinkSM;

        private Accounts _accounts;
        private UserTerminals _userTerminals;
        private ServiceLines _serviceLines;
        private Subscriptions _subscriptions;
        private Products _products;
        private Addresses _addresses;

        public ServiceData(IConfiguration configuration, ILogger<ServiceData> logger, StarlinkSM starlinkSM)
        {
            _logger = logger;
            _configuration = configuration;
            _starlinkSM = starlinkSM;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _starlinkSM.Accounts = await Helper.Accounts.Get(_configuration, _logger);
            _starlinkSM.Addresses = await Helper.Addresses.Get(_configuration, _logger);
            _starlinkSM.Products = await Helper.Products.Get(_configuration, _logger);
            _starlinkSM.ServiceLines = await Helper.ServiceLines.Get(_configuration, _logger);
            _starlinkSM.UserTerminals = await Helper.Terminals.Get(_configuration, _logger);
            _starlinkSM.Subscriptions = await Helper.Subscriptions.Get(_configuration, _logger);

            while (!stoppingToken.IsCancellationRequested) {
                if (_starlinkSM.Settings.Suspended_DataPull) {
                    await Task.Delay(60000);
                }
                else
                {
                    if (_starlinkSM.PollAccounts) {
                        while (_starlinkSM.IsLocked) {
                            _logger.LogDebug("Waiting for telemetry data poll to complete.");
                            await Task.Delay(1000);
                        }

                        while (_starlinkSM.ApiToken is null || _starlinkSM.ApiToken == "") {
                            _logger.LogDebug("Waiting for API token to be fetched.");
                            await Task.Delay(1000);
                        }

                        _logger.LogInformation("Beginning polling for account data.");
                        _starlinkSM.AccountsPollInProgress = true;

                        _accounts = new Accounts(_logger);
                        _userTerminals = new UserTerminals(_logger);
                        _serviceLines = new ServiceLines(_logger);
                        _subscriptions = new Subscriptions(_logger);
                        _products = new Products(_logger);
                        _addresses = new Addresses(_logger);

                        await GetAccounts();
                        await Helper.Accounts.Save(_configuration, _logger, _accounts);

                        Task getAddrs = GetAddresses();
                        Task getProds = GetProducts();
                        Task getSL = GetServiceLines();
                        Task getSubs = GetSubscriptions();
                        Task getUT = GetUserTerminals();

                        Task.WaitAll(getUT, getSL, getSubs, getProds, getAddrs);

                        await Helper.Addresses.Save(_configuration, _logger, _addresses);
                        await Helper.Products.Save(_configuration, _logger, _products);
                        await Helper.ServiceLines.Save(_configuration, _logger, _serviceLines);
                        await Helper.Terminals.Save(_configuration, _logger, _userTerminals);

                        Accounts inMemoryAccounts = _starlinkSM.Accounts;
                        Addresses inMemoryAddresses = _starlinkSM.Addresses;
                        ServiceLines inMemoryServiceLines = _starlinkSM.ServiceLines;
                        Subscriptions inMemorySubscriptions = _starlinkSM.Subscriptions;
                        Products inMemoryProducts = _starlinkSM.Products;
                        UserTerminals inMemoryUserTerminals = _starlinkSM.UserTerminals;

                        inMemoryAccounts = await Helper.Accounts.Sync(_configuration, _logger, _accounts, inMemoryAccounts);
                        inMemoryAddresses = await Helper.Addresses.Sync(_configuration, _logger, _addresses, inMemoryAddresses);
                        inMemoryServiceLines = await Helper.ServiceLines.Sync(_configuration, _logger, _serviceLines, inMemoryServiceLines);
                        inMemorySubscriptions = await Helper.Subscriptions.Sync(_configuration, _logger, _subscriptions, inMemorySubscriptions);
                        inMemoryProducts = await Helper.Products.Sync(_configuration, _logger, _products, inMemoryProducts);
                        inMemoryUserTerminals = await Helper.Terminals.Sync(_configuration, _logger, _userTerminals, inMemoryUserTerminals);

                        _starlinkSM.Accounts = inMemoryAccounts;
                        _starlinkSM.Addresses = inMemoryAddresses;
                        _starlinkSM.ServiceLines = inMemoryServiceLines;
                        _starlinkSM.Subscriptions = inMemorySubscriptions;
                        _starlinkSM.Products = inMemoryProducts;
                        _starlinkSM.UserTerminals = inMemoryUserTerminals;

                        inMemoryAccounts = null;
                        inMemoryAddresses = null;
                        inMemoryServiceLines = null;
                        inMemorySubscriptions = null;
                        inMemoryProducts = null;
                        inMemoryUserTerminals = null;

                        _accounts = null;
                        _addresses = null;
                        _serviceLines = null;
                        _subscriptions = null;
                        _products = null;
                        _userTerminals = null;
                    }
                    else {
                        _logger.LogInformation("PollAccounts is set to false.  Skipping the polling cycle.");
                    }

                    _starlinkSM.AccountsPollInProgress = false;
                    _logger.LogInformation("Done polling for accounts.");

                    await Task.Delay(_configuration.GetValue<int>("AppSettings:PollFrequencies:ServiceData") * 1000);
                }
            }
        }

        private async Task GetAccounts()
        {
            _logger.LogInformation("Getting Accounts.");

            bool isLastPage = false;
            int pageIndex = 0;
            int recordLimit = _recordLimit;

            while (!isLastPage) {
                //_accounts = new Accounts(_logger);
                string response = await GetAPIResponse("accounts?limit=" + recordLimit.ToString() + "&page=" + pageIndex.ToString());
                if (response.Trim().Length > 0) {
                    try {
                        StarlinkModels.Account accountObject;
                        AccountsModel accountItems = JsonConvert.DeserializeObject<AccountsModel>(response);

                        if (accountItems is not null) {
                            foreach (AccountItem account in accountItems.Content.Results) {
                                accountObject = new StarlinkModels.Account() { AccountName = account.AccountName, AccountNumber = account.AccountNumber, RegionCode = account.RegionCode };
                                _accounts.Add(accountObject);
                            }

                            _logger.LogInformation("Fetched {count} accounts.", accountItems.Content.Results.Count);

                            isLastPage = accountItems.Content.IsLastPage;
                        } else {
                            _logger.LogWarning("Accounts list could not be downloaded from the API;");
                        }
                    } catch (Exception ex) {
                        _logger.LogError(ex.Message);
                    }
                    pageIndex++;
                }
                else {
                    isLastPage = true;
                }
            }
        }

        private async Task GetServiceLines()
        {
            bool isLastPage;
            int pageIndex;
            int recordLimit = _recordLimit;
            int totalServiceLineCount = 0;

            _logger.LogInformation("Getting ServiceLines.");

            foreach (StarlinkModels.Account account in _accounts) {
                _logger.LogInformation("Getting Service Lines for {accountNumber}.", account.AccountNumber);

                isLastPage = false;
                pageIndex = 0;
                while (!isLastPage) {
                    string response = await GetAPIResponse("account/" + account.AccountNumber + "/service-lines?limit=" + recordLimit.ToString() + "&page=" + pageIndex.ToString());
                    if (response.Trim().Length > 0) {
                        try {
                            StarlinkModels.ServiceLine serviceLineObject;
                            ServiceLinesModel serviceLineItems = JsonConvert.DeserializeObject<ServiceLinesModel>(response);

                            if (serviceLineItems is not null) {
                                foreach (ServiceLineItem serviceLine in serviceLineItems.Content.Results) {
                                    serviceLineObject = new StarlinkModels.ServiceLine(_logger) { AddressReferenceId = serviceLine.AddressReferenceId, ServiceLineNumber = serviceLine.ServiceLineNumber, Nickname = serviceLine.Nickname, Active = serviceLine.Active, AccountNumber = account.AccountNumber };

                                    _serviceLines.Add(serviceLineObject);
                                }
                                _logger.LogInformation("Fetched {count} service line records for account {accountNumber}.", serviceLineItems.Content.Results.Count, account.AccountNumber);

                                isLastPage = serviceLineItems.Content.IsLastPage;
                                totalServiceLineCount += serviceLineItems.Content.Results.Count;
                            }
                        } catch (Exception ex) {
                            _logger.LogError(ex.Message);
                        }
                        pageIndex++;
                    } else {
                        isLastPage = true;
                    }
                }
            }
            _logger.LogInformation("Fetched {count} service line records for all accounts.", totalServiceLineCount);
        }

        private async Task GetProducts()
        {
            bool isLastPage;
            int pageIndex;
            int recordLimit = _recordLimit;
            int totalProductCount = 0;

            _logger.LogInformation("Getting Products.");

            foreach (StarlinkModels.Account account in _accounts) {
                _logger.LogInformation("Getting Available Products for {accountNumber}.", account.AccountNumber);

                isLastPage = false;
                pageIndex = 0;
                while (!isLastPage) {
                    string response = await GetAPIResponse("account/" + account.AccountNumber + "/subscriptions/available-products?limit=" + recordLimit.ToString() + "&page=" + pageIndex.ToString());
                    if (response.Trim().Length > 0) {
                        try {
                            StarlinkModels.Product productObject;
                            AvailableProductsModel productsItems = JsonConvert.DeserializeObject<AvailableProductsModel>(response);

                            if (productsItems is not null) {
                                foreach (AvailableProductItem productItem in productsItems.Content.Results) {
                                    productObject = new StarlinkModels.Product() { ProductReferenceId = productItem.ProductReferenceId, Name = productItem.Name, Price = productItem.Price, ISOCurrencyCode = productItem.ISOCurrencyCode };

                                    _products.Add(productObject);
                                }
                                _logger.LogInformation("Fetched {count} available product records for account {accountNumber}.", productsItems.Content.Results.Count, account.AccountNumber);

                                isLastPage = productsItems.Content.IsLastPage;
                                totalProductCount += productsItems.Content.Results.Count;
                            }
                        } catch (Exception ex) {
                            _logger.LogError(ex.Message);
                        }
                        pageIndex++;
                    } else {
                        isLastPage = true;
                    }
                }
            }
            _logger.LogInformation("Fetched {count} available product records for all accounts.", totalProductCount);
        }

        private async Task GetUserTerminals()
        {
            bool isLastPage;
            int pageIndex;
            int recordLimit = _recordLimit;
            int totalTerminalCount = 0;

            _logger.LogInformation("Getting User Terminals.");

            foreach (StarlinkModels.Account account in _accounts) {
                _logger.LogInformation("Getting User Terminals for {accountNumber}.", account.AccountNumber);

                isLastPage = false;
                pageIndex = 0;
                while (!isLastPage) {
                    string response = await GetAPIResponse("account/" + account.AccountNumber + "/user-terminals?limit=" + recordLimit.ToString() + "&page=" + pageIndex.ToString());
                    if (response.Trim().Length > 0) {
                        try {
                            StarlinkModels.UserTerminal userTerminalObject;
                            UserTerminalsModel userTerminalItems = JsonConvert.DeserializeObject<UserTerminalsModel>(response);

                            if (userTerminalItems is not null) {
                                foreach (UserTerminalItem userTerminal in userTerminalItems.Content.Results) {
                                    userTerminalObject = new StarlinkModels.UserTerminal() { UserTerminalId = userTerminal.UserTerminalId, KitSerialNumber = userTerminal.KitSerialNumber, DishSerialNumber = userTerminal.DishSerialNumber, ServiceLineNumber = userTerminal.ServiceLineNumber, Active = userTerminal.Active };

                                    _userTerminals.Add(userTerminalObject);
                                }
                                _logger.LogInformation("Fetched {count} terminal records for account {accountNumber}.", userTerminalItems.Content.Results.Count, account.AccountNumber);

                                isLastPage = userTerminalItems.Content.IsLastPage;

                                totalTerminalCount += userTerminalItems.Content.Results.Count;
                            }
                        } catch (Exception ex) {
                            _logger.LogError(ex.Message);
                        }
                        pageIndex++;
                    } else {
                        isLastPage = true;
                    }
                }
            }
            _logger.LogInformation("Fetched {count} terminal records for all accounts.", totalTerminalCount);
        }

        private async Task GetSubscriptions()
        {
            bool isLastPage;
            int pageIndex;
            int recordLimit = _recordLimit;
            int totalSubscriptionCount = 0;

            _logger.LogInformation("Getting Subscriptions.");

            foreach (StarlinkModels.Account account in _accounts) {
                _logger.LogInformation("Getting Subscriptions for {accountNumber}.", account.AccountNumber);

                isLastPage = false;
                pageIndex = 0;
                while (!isLastPage) {
                    string response = await GetAPIResponse("account/" + account.AccountNumber + "/subscriptions?limit=" + recordLimit.ToString() + "&page=" + pageIndex.ToString());
                    if (response.Trim().Length > 0) {
                        try {
                            StarlinkModels.Subscription subscriptionObject;
                            SubscriptionModel subscriptionItems = JsonConvert.DeserializeObject<SubscriptionModel>(response);

                            if (subscriptionItems is not null) {
                                foreach (SubscriptionLineItem subscription in subscriptionItems.Content.Results) {
                                    subscriptionObject = new StarlinkModels.Subscription() { AccountNumber = account.AccountNumber, Description = subscription.Description, ProductReferenceId = subscription.ProductReferenceId, ServiceLineNumber = subscription.ServiceLineNumber, SubscriptionReferenceId = subscription.SubscriptionReferenceId };


                                    if (subscription.StartDate != null)
                                        subscriptionObject.StartDate = DateTime.Parse(subscription.StartDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

                                    if (subscription.NormalizedStartDate != null)
                                        subscriptionObject.NormalizedStartDate = DateTime.Parse(subscription.NormalizedStartDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

                                    if (subscription.EndDate != null)
                                        subscriptionObject.EndDate = DateTime.Parse(subscription.EndDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

                                    if (subscription.ServiceEndDate != null)
                                        subscriptionObject.ServiceEndDate = DateTime.Parse(subscription.ServiceEndDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

                                    _subscriptions.Add(subscriptionObject);
                                }
                                _logger.LogInformation("Fetched {count} subscription records for account {accountNumber}.", subscriptionItems.Content.Results.Count, account.AccountNumber);

                                isLastPage = subscriptionItems.Content.IsLastPage;
                                totalSubscriptionCount += subscriptionItems.Content.Results.Count;
                            }
                        } catch (Exception ex) {
                            _logger.LogError("Error fetching subscriptions. Error: {error}", ex.Message);
                        }
                        pageIndex++;
                    } 
                    else {
                        isLastPage = true;
                    }
                }
            }
            _logger.LogInformation("Fetched {count} subscription records for all accounts.", totalSubscriptionCount);
        }

        private async Task GetAddresses()
        {
            bool isLastPage;
            int pageIndex;
            int recordLimit = _recordLimit;
            int totalAddressCount = 0;

            _logger.LogInformation("Getting Addresses.");

            foreach (StarlinkModels.Account account in _accounts) {
                _logger.LogInformation("Getting Addresses for {accountNumber}.", account.AccountNumber);

                isLastPage = false;
                pageIndex = 0;
                while (!isLastPage) {
                    string response = await GetAPIResponse("account/" + account.AccountNumber + "/addresses?limit=" + recordLimit.ToString() + "&page=" + pageIndex.ToString());
                    if (response.Trim().Length > 0) {
                        try {
                            StarlinkModels.Address addressObject;
                            AddressesModel AddressItems = JsonConvert.DeserializeObject<AddressesModel>(response);

                            if (AddressItems is not null) {
                                foreach (AddressItem addressItem in AddressItems.Content.Results) {
                                    addressObject = new StarlinkModels.Address() { AddressReferenceId = addressItem.AddressReferenceId, AddressLines = addressItem.AddressLines, Locality = addressItem.Locality, AdministrativeArea = addressItem.AdministrativeArea, AdministrativeAreaCode = addressItem.AdministrativeAreaCode, Region = addressItem.Region, RegionCode = addressItem.RegionCode, PostalCode = addressItem.PostalCode, Metadata = addressItem.Metadata, FormattedAddress = addressItem.FormattedAddress, Latitude = addressItem.Latitude, Longitude = addressItem.Longitude };

                                    _addresses.Add(addressObject);
                                }
                                _logger.LogInformation("Fetched {count} available product records for account {accountNumber}.", AddressItems.Content.Results.Count, account.AccountNumber);

                                isLastPage = AddressItems.Content.IsLastPage;
                                totalAddressCount += AddressItems.Content.Results.Count;
                            }
                        } catch (Exception ex) {
                            _logger.LogError(ex.Message);
                        }
                        pageIndex++;
                    } else {
                        isLastPage = true;
                    }
                }
            }
            _logger.LogInformation("Fetched {count} address records for all accounts.", totalAddressCount);
        }

        private async Task<string> GetAPIResponse(string endPoint)
        {
            string token = "";
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

                _logger.LogDebug("Setting up HTTP request headers.");
                token = _starlinkSM.ApiToken;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                _logger.LogDebug("Making HTTP request to " + endPoint);
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(endPoint);
                if (httpResponseMessage.IsSuccessStatusCode) {
                    retVal = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            } catch (Exception _Ex) {
                _logger.LogError("Error during HTTP request [{request}] using API token {token}.  Error:{error}", endPoint, token, _Ex.Message);
                return "";
            }
            return retVal;
        }
    }
}