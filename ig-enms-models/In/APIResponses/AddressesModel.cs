// File Name: AddressesModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/20/2023
//
//
using System;
using System.Collections.Generic;

namespace IG.ENMS.Models.Starlink.In.APIResponses
{
	public class AddressesModel
	{
		public Content Content { get; set; }
		public List<object> Errors { get; set; }       // or List<string> if errors will be string messages
		public List<object> Warnings { get; set; }     // or List<string> if warnings will be string messages
		public List<object> Information { get; set; }  // or List<string> if information will be string messages
		public bool IsValid { get; set; }
	}

	public class Content
	{
		public int TotalCount { get; set; }
		public int PageIndex { get; set; }
		public int Limit { get; set; }
		public bool IsLastPage { get; set; }
		public List<AddressItem> Results { get; set; }
	}

	public class AddressItem
	{
		public string AddressReferenceId { get; set; }
		public List<string> AddressLines { get; set; }
		public string Locality { get; set; }
		public string AdministrativeArea { get; set; }
		public string AdministrativeAreaCode { get; set; }
		public string Region { get; set; }
		public string RegionCode { get; set; }
		public string PostalCode { get; set; }
		public string Metadata { get; set; }  // If this can have other types (not just string), you might need a more complex structure or use object.
		public string FormattedAddress { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}

}

