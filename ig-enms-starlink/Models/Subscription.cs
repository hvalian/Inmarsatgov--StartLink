// File Name: Subscription.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class Subscription
	{
		public string SubscriptionReferenceId { get; set; } = "";
		public string ServiceLineNumber { get; set; } = "";
		public string Description { get; set; } = "";
		public string ProductReferenceId { get; set; } = "";
		public DateTime StartDate { get; set; }
		public DateTime NormalizedStartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime ServiceEndDate { get; set; }

		public string AccountNumber { get; set; } = "";
	}
}

