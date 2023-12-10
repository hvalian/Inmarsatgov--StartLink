// File Name: DataUsage.cs
// Author: rameshvishnubhatla
// Date Created: 8/22/2023
//
//
using System;
namespace IG.ENMS.Starlink.Models
{
	public class DataUsage
	{
		private Dictionary<int, string> _databucketEnum = new Dictionary<int, string>();

		public string ServiceLineNumber { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int DataBucketId { get; set; }
		public double TotalGB { get; set; }

		public DataUsage()
		{

			DatabucketEnum.Add(0, "Unknown");
			DatabucketEnum.Add(1, "Mobile Data");
			DatabucketEnum.Add(2, "Mobile Priority Data");
			DatabucketEnum.Add(3, "Standard Data");
			DatabucketEnum.Add(4, "Priority Data");
			DatabucketEnum.Add(5, "Non-billable");

		}

		public string DataBucket {
			get {
				return DatabucketEnum[DataBucketId];
			}
		}

		public Dictionary<int, string> DatabucketEnum { get => _databucketEnum; set => _databucketEnum = value; }
	}
}

