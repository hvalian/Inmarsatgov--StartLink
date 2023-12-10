// File Name: ServiceLinesModel.cs
// Author: rameshvishnubhatla
// Date Created: 7/24/2023
//
//
using System.Collections.Generic;


namespace IG.ENMS.Models.Starlink.In.APIResponses;

public class ServiceLinesModel
{
	public ServiceLinesContent Content { get; set; }
	public List<string> Errors { get; set; }
	public List<string> Warnings { get; set; }
	public List<string> Information { get; set; }
	public bool IsValid { get; set; }
}

public class ServiceLinesContent
{
	public int TotalCount { get; set; }
	public int PageIndex { get; set; }
	public int Limit { get; set; }
	public bool IsLastPage { get; set; }
	public List<ServiceLineItem> Results { get; set; }
}

public class ServiceLineItem
{
	public string AddressReferenceId { get; set; }
	public string ServiceLineNumber { get; set; }
	public string Nickname { get; set; }
	public bool Active { get; set; }
}


