// File Name: UserTerminalModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/4/2023
//
//
using System;
namespace IG.ENMS.Models.Starlink.Out.WebAPI
{
	public class TerminalDetailsModel
	{
		public UserTerminal UserTerminal { get; set; }
		public Account Account { get; set; }
		public ServiceLine ServiceLine { get; set; }
		public Subscription Subscription { get; set; }

		public TerminalDetailsModel()
		{
			UserTerminal = new UserTerminal();
			Account = new Account();
			ServiceLine = new ServiceLine();
			Subscription = new Subscription();
		}
	}

	public class UserTerminal
	{
		public string UserTerminalId { get; set; }
		public string KitSerialNumber { get; set; }
		public string DishSerialNumber { get; set; }
		public bool Active { get; set; }

	}

	public class Account
	{
		public string AccountNumber { get; set; }
		public string AccountName { get; set; }
	}

	public class ServiceLine
	{
		public string ServiceLineNumber { get; set; }
		public string NickName { get; set; }
		public bool Active { get; set; }
		public Address Address { get; set; }

		public ServiceLine()
		{
			Address = new Address();
		}
	}

	public class Address
	{
		public string AddressReferenceId { get; set; }
		public string FormattedAddress { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}

	public class Subscription
	{
		public string SubscriptionReferenceId { get; set; }
		public string Description { get; set; }
		public string ProductReferenceId { get; set; }
		public string ProductName { get; set; }
		public string	StartDate { get; set; }
		public string NormalizedStartDate { get; set; }
		public string EndDate { get; set; }
		public string ServiceEndDate { get; set; }
	}
}

