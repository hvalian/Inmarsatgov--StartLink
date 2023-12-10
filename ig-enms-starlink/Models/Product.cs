// File Name: Product.cs
// Author: rameshvishnubhatla
// Date Created: 8/17/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class Product
	{
		public string ProductReferenceId { get; set; } = "";
		public string Name { get; set; } = "";
		public double Price { get; set; }
		public string ISOCurrencyCode { get; set; } = "";
	}
}

