// File Name: TelemetryModel.cs
// Author: rameshvishnubhatla
// Date Created: 8/7/2023
//
//
using System.Collections.Generic;
namespace IG.ENMS.Models.Starlink.In.APIResponses
{
	public class TelemetryModel
	{
		public DeviceDataModel Data { get; set; }
		public MetadataModel Metadata { get; set; }
	}

	public class DeviceDataModel
	{
		public List<List<object>> Values { get; set; }
		public Dictionary<string, List<string>> ColumnNamesByDeviceType { get; set; }
	}

	public class EnumsModel
	{
		public Dictionary<string, Dictionary<string, string>> DeviceType { get; set; }
		public Dictionary<string, Dictionary<string, string>> AlertsByDeviceType { get; set; }
	}

	public class MetadataModel
	{
		public EnumsModel Enums { get; set; }
	}
}



