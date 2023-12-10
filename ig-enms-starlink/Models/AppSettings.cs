// File Name: AppSettings.cs
// Author: rameshvishnubhatla
// Date Created: 9/5/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class AppSettings
	{
		public SourceUrls? SourceUrls { get; set; }
		public Credentials? Credentials { get; set; }
		public PollFrequencies? PollFrequencies { get; set; }
		public StateMachine? StateMachine { get; set; }
		public AppLogging? AppLogging { get; set; }
        public CacheSettings? CacheSettings { get; set; }
    }

	public class SourceUrls
	{	
		public string? AuthUrl { get; set; }
		public string? BaseUrl { get; set; }
		public string? TelemetryUrl { get; set; }
	}

	public class Credentials
	{
		public string? ClientId { get; set; }
		public string? ClientSecret { get; set; }
		public string? GrantType { get; set; }
	}

	public class PollFrequencies
	{
		public int? ServiceData { get; set; }
		public int? TelemetryData { get; set; }
		public int? UsageData { get; set; }
	}

	public class StateMachine
	{
		public string? PersistenceLocation { get; set; }
		public string? PersistenceStartDelay { get; set; }
		public int? PersistenceFrequency { get; set; }
	}

	public class AppLogging
	{
		public string? FileLocation { get; set; }
	}

    public class CacheSettings
    {
        public int? UserAccountRefreshMinutes { get; set; }
    }

    public class ConnectionStrings
    {
        public string? IgenmsContext { get; set; }
        public string? IgcmsContext { get; set; }
    }
}

