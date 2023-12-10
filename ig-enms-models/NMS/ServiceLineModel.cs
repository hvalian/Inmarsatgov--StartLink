// File Name: ServiceLineModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/4/2023
//
//
using System;
namespace IG.ENMS.Models.Starlink.NMS
{
	public class ServiceLineModel
	{
		public string AddressReferenceId { get; set; }
		public string ServiceLineNumber { get; set; }
		public string Nickname { get; set; }
		public bool Active { get; set; }

		public Dictionary<string, UserTerminalModel> UserTerminals = new Dictionary<string, UserTerminalModel>();
	}
}

