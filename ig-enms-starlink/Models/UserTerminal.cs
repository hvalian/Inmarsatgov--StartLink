// File Name: UserTerminal.cs
// Author: rameshvishnubhatla
// Date Created: 8/17/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class UserTerminal
	{
		public string UserTerminalId { get; set; } = "";
		public string KitSerialNumber { get; set; } = "";
		public string DishSerialNumber { get; set; } = "";
		public string ServiceLineNumber { get; set; } = "";
		public bool Active { get; set; }
		public DateTime? DeactivationDate { get; set; }
    }
}

