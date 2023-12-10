// File Name: BillingCycleModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/20/2023
//
//
using System;
using System.Collections.Generic;

namespace IG.ENMS.Models.Starlink.In.APIResponses
{
	public class BillingCycleModel
	{
		public BillingCycleContent Content { get; set; }
		public List<object> Errors { get; set; }
		public List<object> Warnings { get; set; }
		public List<object> Information { get; set; }
		public bool IsValid { get; set; }
	}

	public class BillingCycleContent
	{
		public string AccountNumber { get; set; }
		public string AssetNumber { get; set; }
		public object StartDate { get; set; }
		public object EndDate { get; set; }
		public List<BillingCycle> BillingCycles { get; set; }
		public ServicePlan ServicePlan { get; set; }
		public object LastUpdatedAt { get; set; }
	}

	public class BillingCycle
	{
		public object StartDate { get; set; }
		public object EndDate { get; set; }
		public List<DataUsage> DataUsage { get; set; }
		public List<DailyDataUsage> DailyDataUsages { get; set; }
		public List<OverageLine> OverageLines { get; set; }
	}

	public class DataUsage
	{
		public int DataBucket { get; set; }
		public double TotalGB { get; set; }
	}

	public class DailyDataUsage
	{
		public object Date { get; set; }
		public List<DataUsageBin> DataUsageBins { get; set; }
	}

	public class DataUsageBin
	{
		public int DataBucket { get; set; }
		public double TotalGB { get; set; }
	}

	public class OverageLine
	{
		public int Restricted { get; set; }
		public int Unrestricted { get; set; }
		public int PricePerGB { get; set; }
		public int UsageLimitGB { get; set; }
		public int OverageAmountGB { get; set; }
		public int OveragePrice { get; set; }
		public string ProductId { get; set; }
	}

	public class ServicePlan
	{
		public string IsoCurrencyCode { get; set; }
		public bool IsMobilePlan { get; set; }
		public object ActiveFrom { get; set; }
		public string OverageName { get; set; }
		public string OverageDescription { get; set; }
		public bool IsOptedIntoOverage { get; set; }
		public object OverageLineDeactivatedDate { get; set; }
		public OverageLine OverageLine { get; set; }
		public string ProductId { get; set; }
	}
}