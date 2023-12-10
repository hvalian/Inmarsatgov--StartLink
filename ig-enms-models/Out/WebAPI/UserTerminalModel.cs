// File Name: UserTerminalModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/4/2023
//
//
using System;
namespace IG.ENMS.Models.Starlink.Out.WebAPI
{
	public class UserTerminalModel
	{
		public string UserTerminalId { get; set; }
		public string KitSerialNumber { get; set; }
		public string DishSerialNumber { get; set; }
		public string ServiceLineNumber { get; set; }
		public bool Active { get; set; }
	}
}

