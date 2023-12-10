// File Name: TerminalUsage.cs
// Author: rameshvishnubhatla
// Date Created: 8/28/2023
//
//
using System;
namespace IG.ENMS.Models.Starlink.Out.WebAPI
{
	public class TerminalUsage
	{
		public List<UsageInfo> BillingPeriodUsage;
		public List<UsageInfo> DailyUsage;
		public TerminalUsage()
		{
			BillingPeriodUsage = new List<UsageInfo>();
			DailyUsage = new List<UsageInfo>();
		}

		public class UsageInfo
		{
			public string TimeStamp { get; set; }
			public double MobileDataGB { get; set; }
			public double MobilePriorityDataGB { get; set; }
		}
	}
}

