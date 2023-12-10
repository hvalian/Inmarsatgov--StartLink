// File Name: AccountsModel.cs
// Author: rameshvishnubhatla
// Date Created: 7/24/2023
//
//
using System;
using System.Collections.Generic;

namespace StarlinkDC.Models.Starlink
{
	public class AccountsModel
	{
		public AccountsContent Content { get; set; }
		public List<string> Errors { get; set; }
		public List<string> Warnings { get; set; }
		public List<string> Information { get; set; }
		public bool IsValid { get; set; }

	}
	public class AccountsContent
	{
		public int TotalCount { get; set; }
		public int PageIndex { get; set; }
		public int Limit { get; set; }
		public bool IsLastPage { get; set; }
		public List<AccountItem> Results { get; set; }
	}

	public class AccountItem
	{
		public string AccountNumber { get; set; }
		public string RegionCode { get; set; }
		public string AccountName { get; set; }
	}
}

