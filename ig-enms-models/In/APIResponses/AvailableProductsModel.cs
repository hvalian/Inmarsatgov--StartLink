// File Name: AvailableProductsModel.cs
// Author: rameshvishnubhatla
// Date Created: 7/24/2023
//
//
using System.Collections.Generic;


namespace IG.ENMS.Models.Starlink.In.APIResponses;

public class AvailableProductsModel
{
	public AvailableProductsContent Content { get; set; }
	public List<string> Errors { get; set; }
	public List<string> Warnings { get; set; }
	public List<string> Information { get; set; }
	public bool IsValid { get; set; }
}

public class AvailableProductsContent
{
	public int TotalCount { get; set; }
	public int PageIndex { get; set; }
	public int Limit { get; set; }
	public bool IsLastPage { get; set; }
	public List<AvailableProductItem> Results { get; set; }
}

public class AvailableProductItem
{
	public string ProductReferenceId { get; set; }
	public string Name { get; set; }
	public double Price { get; set; }
	public string ISOCurrencyCode { get; set; }
}


