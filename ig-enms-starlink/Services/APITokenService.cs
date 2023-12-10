// File Name: APITokenService.cs
// Author: rameshvishnubhatla
// Date Created: 8/14/2023
//
//
using System;
using IG.ENMS.Models.Starlink.In.APIResponses;
using IG.ENMS.Starlink.StateMachines;
using Newtonsoft.Json;
using IG.ENMS.Starlink.Services.Pollers;
using IG.ENMS.Models.Starlink.In.APIResponses;

namespace IG.ENMS.Starlink.Services
{
	public class APITokenService : BackgroundService
	{
		readonly ILogger<APITokenService> _logger;
		readonly IConfiguration _configuration;
		readonly StarlinkSM _starlinkSM;

		private AccessTokenModel _accessToken;
		private DateTime _tokenExpiration;

		public APITokenService(IConfiguration configuration, ILogger<APITokenService> logger, StarlinkSM starlinkSM)
		{
			_logger = logger;
			_configuration = configuration;
			_starlinkSM = starlinkSM;
		}

		public DateTime TokenExpiration { get => _tokenExpiration; set => _tokenExpiration = value; }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested) {
				if (TokenExpiration < DateTime.Now) {
					_logger.LogInformation("Refreshing API Token.");

					_accessToken = await GetAPIToken();

					if (_accessToken is not null) {
						_logger.LogInformation("New API Token received.");

						_starlinkSM.ApiToken = _accessToken.Access_token;

						//subtract 30 seconds to prevent a token expiration in the middle of an API call.
						TokenExpiration = DateTime.Now.AddSeconds(_accessToken.Expires_in - 30);

						//wait for 10 seconds
						await Task.Delay(10000);
					} else {
						_logger.LogInformation("Could not get new API Token retrying in 500 ms.");

						//blank out the ApiToken in the state machine so that the data getter calls do not use an expired token.
						_starlinkSM.ApiToken = "";

						await Task.Delay(500);
					}

				}

			}
		}

		private async Task<AccessTokenModel> GetAPIToken()
		{
			Dictionary<string, string> authFormData = new Dictionary<string, string>();

			HttpClient httpClient = new HttpClient();

			authFormData.Add("client_id", _configuration.GetValue<string>("AppSettings:Credentials:ClientId"));
			authFormData.Add("client_secret", _configuration.GetValue<string>("AppSettings:Credentials:ClientSecret"));
			authFormData.Add("grant_type", _configuration.GetValue<string>("AppSettings:Credentials:GrantType"));

			string authUrl = _configuration.GetValue<string>("AppSettings:SourceUrls:AuthUrl");
			if (authUrl.Trim().Length == 0) {
				_logger.LogCritical("Authorization URL is missing in the appsettings file.");
				return null;
			}

			FormUrlEncodedContent content = new FormUrlEncodedContent(authFormData);
			try {
				HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(authUrl, content);

				if (!httpResponseMessage.IsSuccessStatusCode)
					return null;
				else
					return JsonConvert.DeserializeObject<AccessTokenModel>(await httpResponseMessage.Content.ReadAsStringAsync());
			}
			catch (Exception _Ex) {
				_logger.LogError("Error fetching API Token. Error Message: {error}", _Ex.Message);
				return null;
			}
		}
	}
}

