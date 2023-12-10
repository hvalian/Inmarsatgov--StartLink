// File Name: UserTerminalsModel.cs
// Author: rameshvishnubhatla
// Date Created: 7/31/2023
//
//
using System;
using System.Collections.Generic;

namespace StarlinkDC.Models.Starlink
{
	public class UserTerminalsModel
	{
		public UserTerminalContent Content { get; set; }
		public List<string> Errors { get; set; }
		public List<string> Warnings { get; set; }
		public List<string> Information { get; set; }
		public bool IsValid { get; set; }
	}

	public class UserTerminalContent
	{
		public int TotalCount { get; set; }
		public int PageIndex { get; set; }
		public int Limit { get; set; }
		public bool IsLastPage { get; set; }
		public List<UserTerminalItem> Results { get; set; }
	}

	public class UserTerminalItem
	{
		public string UserTerminalId { get; set; }
		public string KitSerialNumber { get; set; }
		public string DishSerialNumber { get; set; }
		public string ServiceLineNumber { get; set; }
		public bool Active { get; set; }
	}

}

