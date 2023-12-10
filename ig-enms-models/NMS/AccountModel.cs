// File Name: AccountModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/4/2023
//
//
using System;
namespace IG.ENMS.Models.Starlink.NMS
{
	public class AccountModel
	{
		public string AccountNumber { get; set; }
		public string RegionCode { get; set; }
		public string AccountName { get; set; }

		public Dictionary<string, ServiceLineModel> ServiceLines = new Dictionary<string, ServiceLineModel>();
		public Dictionary<string, UserTerminalModel> UserTerminals = new Dictionary<string, UserTerminalModel>();
	}
}

