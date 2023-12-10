// File Name: ServicePlan.cs
// Author: rameshvishnubhatla
// Date Created: 8/22/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class ServicePlan
	{
		public string IsoCurrencyCode { get; set; } = "";
		public bool IsMobilePlan { get; set; }
        public DateTime ActiveFrom { get; set; }
        public string OverageName { get; set; } = "";
		public string OverageDescription { get; set; } = "";
		public bool IsOptedIntoOverage { get; set; }
        public DateTime overageLineDeactivatedDate  { get; set; }
        public double UsageLimitGB { get; set; }
		public double OverageAmountGB { get; set; }
        public double PricePerGB { get; set; }
        public double OveragePrice { get; set; }
	}
}
