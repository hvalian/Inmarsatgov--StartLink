// File Name: StarlinkAPIPoller.cs
// Author: rameshvishnubhatla
// Date Created: 8/1/2023
//
//
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using IG.ENMS.Models.Starlink.In.APIResponses;
using IG.ENMS.Models.Starlink.NMS;
using IG.ENMS.Starlink.StateMachines;
using IG.ENMS.Starlink.Data;
using Microsoft.Extensions.Configuration;

namespace StarlinkDC.Services;

public class StarlinkAPIPoller : BackgroundService
{
	private readonly ILogger<StarlinkAPIPoller> _logger;
	private readonly IConfiguration _configuration;

	private readonly StarlinkSM _starlinkSM;
	//private StarlinkData _starlinkData;

	private string _authUrl = "https://api.starlink.com/auth/connect/token";
	private string _url = "https://web-api.starlink.com/enterprise/v1/";
	private string _telemetryUrl = "https://web-api.starlink.com/telemetry/stream/v1/telemetry";

	private string clientId = "60f284d0-78e4-4bfd-8f44-1dde30f4fd01";
	private string clientSecret = "fegzYw-notga5-codhij";
	private string grantType = "client_credentials";

	private AccessTokenModel _accessToken;
	private DateTime _tokenExpiration = new DateTime();

	private Dictionary<string, string> authFormData = new Dictionary<string, string>();

	public StarlinkAPIPoller(IConfiguration configuration, ILogger<StarlinkAPIPoller> logger, StarlinkSM starlinkSM)
	{
		_logger = logger;
		_starlinkSM = starlinkSM;
		//_starlinkData = starlinkData;

		_configuration = configuration;

		authFormData.Add("client_id", clientId);
		authFormData.Add("client_secret", clientSecret);
		authFormData.Add("grant_type", grantType);
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		Task accountDataTask = PollAccountDataAsync(stoppingToken);
		//Task telemetryTask = PollTelemetryDataAsync(stoppingToken);

		await Task.WhenAll(accountDataTask); //, telemetryTask);

		Console.WriteLine("Big Task exiting... on console.");

		_logger.LogInformation("Big Task exiting");
	}

	private async Task PollAccountDataAsync(CancellationToken stoppingToken)
	{
		DateTime startTime = DateTime.Now;
		DateTime endTime = DateTime.Now;
		//AccountData accountData = new AccountData();
		Account accountObject;
		ServiceLineModel serviceLineObject;
		UserTerminalModel userTerminalObject;

		while (!stoppingToken.IsCancellationRequested) {
			startTime = DateTime.Now;
			int totalAccounts = 0;
			int totalServiceLines = 0;
			int totalUserTerminals = 0;
			//AccessTokenModel accessToken = await GetAPIToken();

			int x = _configuration.GetSection("AppSettings").GetValue<int>("PollFrequencies:AccountData");
			_logger.LogInformation("PollFrequencies.AccountData:{x}", x);

			foreach (AccountItem account in (await GetAccounts()).Content.Results) {
				//accountData = new AccountData();
				//accountData.AccountInfo = account;
				//_starlinkData.Accounts.Add(accountData);

				accountObject = new Account() { AccountName = account.AccountName, AccountNumber = account.AccountNumber, RegionCode = account.RegionCode };
				_starlinkSM.Accounts.Add(accountObject);

				totalAccounts++;

				//ServiceLinesModel serviceLinesResponse = await GetAccountServiceLines(account.AccountNumber);
				//if (serviceLinesResponse.Content.Results is not null)
				//	foreach (ServiceLineItem serviceLine in serviceLinesResponse.Content.Results) {
				//		serviceLineObject = new ServiceLineModel() { AddressReferenceId = serviceLine.AddressReferenceId, Active = serviceLine.Active, Nickname = serviceLine.Nickname, ServiceLineNumber = serviceLine.ServiceLineNumber };

				//		_starlinkData.AddServiceLine(accountObject.AccountName, serviceLineObject);
				//		//accountData.ServiceLines.Add(serviceLine);
				//	}

				//totalServiceLines += serviceLinesResponse.Content.TotalCount;

				//UserTerminalsModel userTerminalsModel = await GetAccountUserTerminals(account.AccountNumber);
				//if (userTerminalsModel.Content.Results is not null)
				//	foreach (UserTerminalItem userTerminal in userTerminalsModel.Content.Results) {
				//		userTerminalObject = new UserTerminalModel() { Active = userTerminal.Active, DishSerialNumber = userTerminal.DishSerialNumber, KitSerialNumber = userTerminal.KitSerialNumber, ServiceLineNumber = userTerminal.ServiceLineNumber, UserTerminalId = userTerminal.UserTerminalId };

				//		_starlinkData.AddUserTerminal(accountObject.AccountNumber, userTerminalObject);
				//		//accountData.UserTerminals.Add(userTerminal);
				//	}

				//totalUserTerminals += userTerminalsModel.Content.TotalCount;

				//_logger.LogInformation("\tFound Service Lines/User Terminals: " + serviceLinesResponse.Content.TotalCount + "/" + userTerminalsModel.Content.TotalCount + " for account " + account.AccountNumber);
			}
			_logger.LogInformation("Total account:" + totalAccounts);
			//_logger.LogInformation("Total service lines:" + totalServiceLines);
			//_logger.LogInformation("Total user terminals:" + totalUserTerminals);
			//endTime = DateTime.Now;
			//_logger.LogInformation("Time to fetch: " + (endTime - startTime).Milliseconds);
			await Task.Delay(1000 * 15);
		}
	}

	//private async Task PollTelemetryDataAsync(CancellationToken stoppingToken)
	//{
	//	while (!stoppingToken.IsCancellationRequested) {
	//		_logger.LogInformation("Polling for telemetry");
	//		TelemetryModel telemetryModel = await GetTelemetry("ACC-2663122-32070-99");

	//		//_logger.LogDebug(telemetryModel.Data.ToString());
	//		await Task.Delay(1000 * 10);
	//	}
	//}

	//private async Task<TelemetryModel> GetTelemetry(string AccountNumber)
	//{
	//	_logger.LogInformation("Getting Telemetry.");

	//	HttpClient httpClient = new HttpClient();

	//	AccessTokenModel accessToken = await APIToken();
	//	httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Access_token);

	//	var bodyContent = new {
	//		accountNumber = AccountNumber,
	//		batchSize = 1000,
	//		maxLingerMs = 500
	//	};

	//	string bodyJson = JsonConvert.SerializeObject(bodyContent);
	//	var content = new StringContent(bodyJson, System.Text.Encoding.UTF8, "application/json");

	//	HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_telemetryUrl, content);
	//	if (httpResponseMessage.IsSuccessStatusCode)
	//		try {
				
	//			string response = await httpResponseMessage.Content.ReadAsStringAsync();

	//			if (response is not null) {
	//				JObject responseData = JObject.Parse(response);
	//				JToken data = responseData.SelectToken("data.values");
	//				JToken columnNames = responseData.SelectToken("data.columnNamesByDeviceType.u");
	//				JToken alertLookup = responseData.SelectToken("metadata.enums.AlertsByDeviceType.u");

	//				List<JToken> alertList = alertLookup.Values().ToList<JToken>();
	//				foreach(JValue item in alertList) {
	//					Console.WriteLine(item.Value);
	//				}

	//			}
	//			return JsonConvert.DeserializeObject<TelemetryModel>(response);
	//		} catch (Exception ex) {
	//			Console.WriteLine(ex.Message);
	//			return null;
	//		}
	//	else
	//		return null;
	//}

	private async Task<AccountsModel> GetAccounts()
	{
		_logger.LogInformation("Getting Accounts.");
		string text = await GetAPIResponse(_url + "accounts");
		if (text.Trim().Length == 0) {
			return null;
		}
		try {
			return JsonConvert.DeserializeObject<AccountsModel>(text);
		} catch (Exception ex) {
			Console.WriteLine(ex.Message);
			return null;
		}
	}

	private async Task<ServiceLinesModel> GetAccountServiceLines(string accountNumber)
	{
		_logger.LogInformation("Getting Account Service Lines for {accountNumber}.", accountNumber);
		string text = await GetAPIResponse(_url + "account/" + accountNumber + "/service-lines");
		if (text.Trim().Length == 0) {
			return null;
		}
		try {
			return JsonConvert.DeserializeObject<ServiceLinesModel>(text);
		} catch (Exception ex) {
			Console.WriteLine(ex.Message);
			return null;
		}
	}

	private async Task<UserTerminalsModel> GetAccountUserTerminals(string accountNumber)
	{
		_logger.LogInformation("Getting Account User Terminals for {accountNumber}.", accountNumber);
		string text = await GetAPIResponse(_url + "account/" + accountNumber + "/user-terminals?active=true");
		if (text.Trim().Length == 0) {
			return null;
		}
		try {
			return JsonConvert.DeserializeObject<UserTerminalsModel>(text);
		} catch (Exception ex) {
			Console.WriteLine(ex.Message);
			return null;
		}
	}

	private async Task<string> GetAPIResponse(string endPoint)
	{
		string retVal = "";
		HttpClient httpClient = new HttpClient();
		httpClient.BaseAddress = new Uri(endPoint);

		AccessTokenModel accessToken = await APIToken();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Access_token);
		HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(endPoint);
		if (httpResponseMessage.IsSuccessStatusCode) {
			retVal = await httpResponseMessage.Content.ReadAsStringAsync();
		}
		return retVal;
	}

	private async Task<AccessTokenModel> APIToken()
	{
		if (_tokenExpiration < DateTime.Now) {
			_accessToken = await GetAPIToken();
			_tokenExpiration = DateTime.Now.AddSeconds(_accessToken.Expires_in - 30);
		}

		return _accessToken;
	}

	private async Task<AccessTokenModel> GetAPIToken()
	{
		HttpClient httpClient = new HttpClient();
		FormUrlEncodedContent content = new FormUrlEncodedContent(authFormData);
		HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_authUrl, content);
		return (!httpResponseMessage.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<AccessTokenModel>(await httpResponseMessage.Content.ReadAsStringAsync());
	}
}

