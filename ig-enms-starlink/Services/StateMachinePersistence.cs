// File Name: APITokenService.cs
// Author: rameshvishnubhatla
// Date Created: 8/14/2023
//
//
using IG.ENMS.Starlink.Data;
using IG.ENMS.Starlink.StateMachines;
using Newtonsoft.Json;

namespace IG.ENMS.Starlink.Services
{
	public class StateMachinePersistence : BackgroundService
	{
		readonly ILogger<StateMachinePersistence> _logger;
		readonly IConfiguration _configuration;
		readonly StarlinkSM _starlinkSM;

		public StateMachinePersistence(IConfiguration configuration, ILogger<StateMachinePersistence> logger, StarlinkSM starlinkSM)
		{
			_logger = logger;
			_configuration = configuration;
			_starlinkSM = starlinkSM;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			int persistenceStartDelay = _configuration.GetValue<int>("AppSettings:StateMachine:PersistenceStartDelay") * 1000;

			//if configured to wait before starting the service loop, wait.
			//this will give the time required for other services to start and catch up to serve
			if (persistenceStartDelay > 0) {
				_logger.LogInformation("Waiting for PersistenceStartDelay [{persistenceStartDelay}].", persistenceStartDelay);
				await Task.Delay(persistenceStartDelay);
			}
			while (!stoppingToken.IsCancellationRequested) {
				if (!_starlinkSM.PersistStateMachine) {
					await Task.Delay(1000);
					continue;
				}

				while (_starlinkSM.IsLocked) {
					await Task.Delay(200);
				}

				_starlinkSM.StateMachinePersistenceInProgress = true;

                if (!_starlinkSM.Settings.Suspended_SaveToDisk)
                {
					await SaveStateMachineToDisk();
                }

				_starlinkSM.StateMachinePersistenceInProgress = false;
				await Task.Delay(_configuration.GetValue<int>("AppSettings:StateMachine:PersistenceFrequency") * 1000);
			}

		}

		private async Task SaveStateMachineToDisk()
		{
			string filePath = _configuration.GetValue<string>("AppSettings:StateMachine:PersistenceLocation");

			if (filePath is null || filePath =="") {
				filePath = "persistence";
				_logger.LogWarning("AppSettings:StateMachine:PersistenceLocation is null.  Using default [{filepath}]", filePath);
			}

			if (!Directory.Exists(filePath)) {
				Directory.CreateDirectory(filePath);
			}

			string timespan = DateTime.Now.Ticks.ToString();

			_logger.LogInformation("Begin persisting state machine to disk at {filePath}.", filePath);
			//try {
				//_logger.LogInformation("Persisting Accounts.");
				//var jsonString = JsonConvert.SerializeObject(_starlinkSM.Accounts);
				//await File.WriteAllTextAsync(filePath + "/accounts-" + timespan + ".json", jsonString);

				//_logger.LogInformation("Persisting Service Lines.");
				//jsonString = JsonConvert.SerializeObject(_starlinkSM.ServiceLines);
				//await File.WriteAllTextAsync(filePath + "/service-lines-" + timespan + ".json", jsonString);

				//_logger.LogInformation("Persisting Addresses.");
				//jsonString = JsonConvert.SerializeObject(_starlinkSM.Addresses);
				//await File.WriteAllTextAsync(filePath + "/addresses-" + timespan + ".json", jsonString);

				//_logger.LogInformation("Persisting User Terminals.");
				//jsonString = JsonConvert.SerializeObject(_starlinkSM.UserTerminals);
				//await File.WriteAllTextAsync(filePath + "/user-terminals-" + timespan + ".json", jsonString);

				//_logger.LogInformation("Persisting Subscriptions.");
				//jsonString = JsonConvert.SerializeObject(_starlinkSM.Subscriptions);
				//await File.WriteAllTextAsync(filePath + "/subscriptions-" + timespan + ".json", jsonString);

				//_logger.LogInformation("Persisting Available Products.");
				//jsonString = JsonConvert.SerializeObject(_starlinkSM.Products);
				//await File.WriteAllTextAsync(filePath + "/available-products-" + timespan + ".json", jsonString);

			//	_logger.LogInformation("Done persisting state machine to disk at {filePath}.", filePath);
			//} catch (Exception _Ex) {
			//	_logger.LogError("Error persisting state machine to {filePath}.  Error: {error}", filePath, _Ex.Message);
			//}

            try
            {
                _logger.LogInformation("Persisting Telemetry.");
                var jsonString = JsonConvert.SerializeObject(_starlinkSM.NewTelemetryItems);
                await File.WriteAllTextAsync(filePath + "/telemetry-" + timespan + ".json", jsonString);

                _logger.LogInformation("Done persisting Telemetry to disk at {filePath}.", filePath);
            }
            catch (Exception _Ex)
            {
                _logger.LogError("Error persisting Telemetry to {filePath}.  Error: {error}", filePath, _Ex.Message);
            }
            _starlinkSM.NewTelemetryItems = null;

            try
            {
                _logger.LogInformation("Persisting Billing Cycle Usage.");
                var jsonString = JsonConvert.SerializeObject(_starlinkSM.NewBillingCycleUsages);
                await File.WriteAllTextAsync(filePath + "/usage-billingcycle-" + timespan + ".json", jsonString);

                _logger.LogInformation("Done persisting Billing Cycle Usage to disk at {filePath}.", filePath);
            }
            catch (Exception _Ex)
            {
                _logger.LogError("Error persisting Billing Cycle Usage to {filePath}.  Error: {error}", filePath, _Ex.Message);
            }
            _starlinkSM.NewBillingCycleUsages = null;

            try
            {
                _logger.LogInformation("Persisting Daily Data Usage.");
                var jsonString = JsonConvert.SerializeObject(_starlinkSM.NewDailyDataUsages);
                await File.WriteAllTextAsync(filePath + "/usage-dailydata-" + timespan + ".json", jsonString);

                _logger.LogInformation("Done persisting Daily Data Usage to disk at {filePath}.", filePath);
            }
            catch (Exception _Ex)
            {
                _logger.LogError("Error persisting Daily Data Usage to {filePath}.  Error: {error}", filePath, _Ex.Message);
            }
            _starlinkSM.NewDailyDataUsages = null;

            return;

		}
	}
}

