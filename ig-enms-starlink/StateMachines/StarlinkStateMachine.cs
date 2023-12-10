// File Name: StarlinkData.cs
// Author: rameshvishnubhatla
// Date Created: 8/3/2023
//
//
using System;
using WebAPIModels = IG.ENMS.Models.Starlink.Out.WebAPI;
using IG.ENMS.Starlink.Data;
using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Services;
using Newtonsoft.Json;

namespace IG.ENMS.Starlink.StateMachines
{
    public class StarlinkSM
    {
        readonly ILogger _logger;
        readonly IConfiguration _configuration;

        private bool _pollTelemetry = false;
        private bool _pollAccounts = false;
        private bool _pollUsage = false;

        private bool _persistStateMachine = false;
        private bool _stateMachinePersistenceInProgress = false;

        private bool _telemetryPollInProgress = false;
        private bool _accountsPollInProgress = false;
        private bool _usagePollInProgress = false;

        private string _apiToken = "";
        private Accounts _accounts;
        private UserTerminals _userTerminals;
        private ServiceLines _serviceLines;
        private Subscriptions _subscriptions;
        private Products _products;
        private Addresses _addresses;
        private DataUsages _dailyDataUsages;
        private DataUsages _billingCycleUsages;
        private TelemetryItems _telemetryItems;

        private DataUsages _newDailyDataUsages;
        private DataUsages _newBillingCycleUsages;
        private TelemetryItems _newTelemetryItems;
        private Settings _settings;

        public StarlinkSM(ILogger<StarlinkSM> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _settings = new Settings(_configuration);

            _accounts = new Accounts(_logger);
            _userTerminals = new UserTerminals(_logger);
            _serviceLines = new ServiceLines(_logger);
            _subscriptions = new Subscriptions(_logger);
            _products = new Products(_logger);
            _addresses = new Addresses(_logger);
            _dailyDataUsages = new DataUsages(_logger);
            _billingCycleUsages = new DataUsages(_logger);
            _telemetryItems = new TelemetryItems(_configuration,_logger, _settings);

            _newBillingCycleUsages = new DataUsages(_logger);
            _newDailyDataUsages = new DataUsages(_logger);
            _newTelemetryItems = new TelemetryItems(_configuration, _logger, _settings);

            PersistStateMachine = false;
            PollAccounts = true;
            PollTelemetry = true;
            PollUsage = true;

            //start persistence service
            PersistStateMachine = true;
        }

        public bool IsLocked { get => (TelemetryPollInProgress || AccountsPollInProgress || UsagePollInProgress || StateMachinePersistenceInProgress); }

        public bool PersistStateMachine { get => _persistStateMachine; set => _persistStateMachine = value; }
        public bool StateMachinePersistenceInProgress { get => _stateMachinePersistenceInProgress; set => _stateMachinePersistenceInProgress = value; }

        public bool PollTelemetry { get => _pollTelemetry; set => _pollTelemetry = value; }
        public bool TelemetryPollInProgress { get => _telemetryPollInProgress; set => _telemetryPollInProgress = value; }

        public bool PollAccounts { get => _pollAccounts; set => _pollAccounts = value; }
        public bool AccountsPollInProgress { get => _accountsPollInProgress; set => _accountsPollInProgress = value; }

        public bool PollUsage { get => _pollUsage; set => _pollUsage = value; }
        public bool UsagePollInProgress { get => _usagePollInProgress; set => _usagePollInProgress = value; }

        public string ApiToken { get => _apiToken; set => _apiToken = value; }
        public Accounts Accounts { get => _accounts; set => _accounts = value; }
        public UserTerminals UserTerminals { get => _userTerminals; set => _userTerminals = value; }
        public ServiceLines ServiceLines { get => _serviceLines; set => _serviceLines = value; }
        public Subscriptions Subscriptions { get => _subscriptions; set => _subscriptions = value; }
        public Products Products { get => _products; set => _products = value; }
        public Addresses Addresses { get => _addresses; set => _addresses = value; }
        public DataUsages DailyDataUsages { get => _dailyDataUsages; set => _dailyDataUsages = value; }
        public DataUsages BillingCycleUsages { get => _billingCycleUsages;  set => _billingCycleUsages = value; }
        public TelemetryItems TelemetryItems { get => _telemetryItems; set => _telemetryItems = value; }

        public DataUsages NewBillingCycleUsages { get => _newBillingCycleUsages; set => _newBillingCycleUsages = value; }
        public DataUsages NewDailyDataUsages { get => _newDailyDataUsages; set => _newDailyDataUsages = value; }
        public TelemetryItems NewTelemetryItems { get => _newTelemetryItems; set => _newTelemetryItems = value; }

        public Settings Settings { get => _settings; set => _settings = value; }
    }
}

