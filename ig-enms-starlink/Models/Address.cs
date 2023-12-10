// File Name: Account.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class Address
	{
		public string AddressReferenceId { get; set; } = "";
		public List<string> AddressLines { get; set; }
		public string Locality { get; set; } = "";
		public string AdministrativeArea { get; set; } = "";
		public string AdministrativeAreaCode { get; set; } = "";
		public string Region { get; set; } = "";
        public string RegionCode { get; set; } = "";
        public string PostalCode { get; set; } = "";
		public string Metadata { get; set; } = "";
		public string FormattedAddress { get; set; } = "";
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public Address()
		{
			AddressLines = new List<string>();
		}
	}
}

