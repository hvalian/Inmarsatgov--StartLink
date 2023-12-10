// File Name: SubscriptionModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/18/2023
//
//
using System;
namespace IG.ENMS.Models.Starlink.In.APIResponses
{
	public class SubscriptionModel
	{
		public SubscriptionsContent Content { get; set; }
		public List<string> Errors { get; set; }
		public List<string> Warnings { get; set; }
		public List<string> Information { get; set; }
		public bool IsValid { get; set; }
	}

	public class SubscriptionsContent
	{
		public int TotalCount { get; set; }
		public int PageIndex { get; set; }
		public int Limit { get; set; }
		public bool IsLastPage { get; set; }
		public List<SubscriptionLineItem> Results { get; set; }
	}

	public class SubscriptionLineItem
	{
		public string SubscriptionReferenceId { get; set; }
		public string ServiceLineNumber { get; set; }
		public string Description { get; set; }
		public string ProductReferenceId { get; set; }
		public string StartDate { get; set; }
		public string NormalizedStartDate { get; set; }
		public string EndDate { get; set; }
		public string ServiceEndDate { get; set; }
		public string DelayedProductId { get; set; }
		public string OptInProductId { get; set; }

	}
}

