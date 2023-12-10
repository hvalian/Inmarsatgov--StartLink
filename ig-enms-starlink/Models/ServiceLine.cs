// File Name: UserTerminal.cs
// Author: rameshvishnubhatla
// Date Created: 8/17/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class ServiceLine
	{
		private readonly ILogger _logger;

		public string AddressReferenceId { get; set; } = "";
		public string ServiceLineNumber { get; set; } = "";
		public string Nickname { get; set; } = "";
		public bool Active { get; set; } = false;

		public string AccountNumber { get; set; } = "";

		public ServicePlan ServicePlan { get; set; }

		public ServiceLine(ILogger logger)
		{
			_logger = logger;

			ServicePlan = new ServicePlan();
		}
	}
}

