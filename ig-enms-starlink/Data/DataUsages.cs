// File Name: DataUsages.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System;
using System.Collections;
using System.Security.Principal;
using IG.ENMS.Starlink.Models;

namespace IG.ENMS.Starlink.Data
{
	public class DataUsages : IEnumerable<Dictionary<DateTime, DataUsage>>
	{
		private readonly ILogger _logger;
		private Dictionary<string, Dictionary<DateTime, DataUsage>> _DataUsageList = new Dictionary<string, Dictionary<DateTime, DataUsage>>();

		public DataUsages(ILogger Logger)
		{
			_logger = Logger;
		}

		public bool Add(DataUsage DataUsage)
		{
			_logger.LogDebug("Entering Add(DataUsage) with argument {DataUsage}.", DataUsage);
			try {
                DateTime dictionaryKey = Helper.Usages.GetKey(DataUsage);
                
				if (_DataUsageList.ContainsKey(DataUsage.ServiceLineNumber)) {
                    if (_DataUsageList[DataUsage.ServiceLineNumber].ContainsKey(dictionaryKey)) {
						_logger.LogDebug("DataUsage {startDate} already exists.  Replacing it with new data.", DataUsage.StartDate);
						_DataUsageList[DataUsage.ServiceLineNumber][dictionaryKey] = DataUsage;
						return true;
					}
					else {
						_DataUsageList[DataUsage.ServiceLineNumber].Add(dictionaryKey, DataUsage);
						return true;
					}
				}

				_DataUsageList.Add(DataUsage.ServiceLineNumber, new Dictionary<DateTime, DataUsage>());
				_DataUsageList[DataUsage.ServiceLineNumber].Add(dictionaryKey, DataUsage);

				_logger.LogDebug("DataUsage {DataUsage} added successfully.", DataUsage);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding DataUsage. {DataUsage}. Error: {errorMessage}", DataUsage, _Ex.Message);

				return false;
			}
		}


		public Dictionary<DateTime, DataUsage> Get (string ServiceLineNumber)
        {
            if (_DataUsageList.ContainsKey(ServiceLineNumber))
                return _DataUsageList[ServiceLineNumber];
            else
                return null;
        }

        IEnumerator<Dictionary<DateTime, DataUsage>> IEnumerable<Dictionary<DateTime, DataUsage>>.GetEnumerator()
		{
			return _DataUsageList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _DataUsageList.GetEnumerator();
		}

		public Dictionary<string, Dictionary<DateTime, DataUsage>> GetDataUsageList()
		{
			return _DataUsageList;
		}
	}
}
