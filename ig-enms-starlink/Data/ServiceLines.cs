// File Name: ServiceLines.cs
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
	public class ServiceLines : IEnumerable<ServiceLine>
	{
		private readonly ILogger _logger;
		private Dictionary<string, ServiceLine> _serviceLineList;

		public ServiceLines(ILogger Logger)
		{
			_logger = Logger;
			_serviceLineList = new Dictionary<string, ServiceLine>();
		}

		public bool Clear()
		{
			try {
				_serviceLineList.Clear();
				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error clearing service line list dictionary. Error: {errorMessage}", _Ex.Message);
				return false;
			}
		}

		public bool Add(ServiceLine serviceLine)
		{
			_logger.LogDebug("Entering Add(ServiceLine) with argument {serviceLine}.", serviceLine);
			try {
				if (_serviceLineList.ContainsKey(serviceLine.ServiceLineNumber)) {
					_logger.LogDebug("ServiceLine {serviceLineNumber} already exists.  Replacing it with new data.", serviceLine.ServiceLineNumber);
					_serviceLineList[serviceLine.ServiceLineNumber] = serviceLine;
					return true;
				}

				_serviceLineList.Add(serviceLine.ServiceLineNumber, serviceLine);

				_logger.LogDebug("ServiceLine {serviceLine} added successfully.", serviceLine);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding serviceLine. {serviceLine}. Error: {errorMessage}", serviceLine, _Ex.Message);

				return false;
			}
		}

		public bool Add(string AddressReferenceId, string ServiceLineNumber, string NickName, bool Active, string AccountNumber)
		{
			_logger.LogDebug("Entering Add(ServiceLine) with arguments AddressReferenceId:{addressReferenceId}, ServiceLineNumber:{serviceLineNumber}, NickName:{nickName}, Active:{active}, AccountNumber:{accountNumber}.", AddressReferenceId, ServiceLineNumber, NickName, Active, AccountNumber);

			try {
				if (_serviceLineList.ContainsKey(ServiceLineNumber)) {
					_logger.LogDebug("ServiceLine {serviceLineNumber} already exists.  Replacing it with new data.", ServiceLineNumber);

					_serviceLineList[ServiceLineNumber] = new ServiceLine(_logger) { AddressReferenceId = AddressReferenceId, ServiceLineNumber = ServiceLineNumber, Nickname = NickName, Active = Active, AccountNumber = AccountNumber };

					return true;
				}

				_serviceLineList.Add(ServiceLineNumber, new ServiceLine(_logger) { AddressReferenceId = AddressReferenceId, ServiceLineNumber = ServiceLineNumber, Nickname = NickName, Active = Active, AccountNumber = AccountNumber });

				_logger.LogDebug("ServiceLine for AddressReferenceId:{addressReferenceId}, ServiceLineNumber:{serviceLineNumber}, NickName:{nickName}, Active:{active}, AccountNumber:{accountNumber} added successfully.", AddressReferenceId, ServiceLineNumber, NickName, Active, AccountNumber);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding serviceLine with arguments AddressReferenceId:{addressReferenceId}, ServiceLineNumber:{serviceLineNumber}, NickName:{nickName}, Active:{active}, AccountNumber:{accountNumber}.  Error: {errorMessage}", AddressReferenceId, ServiceLineNumber, NickName, Active, AccountNumber, _Ex.Message);

				return false;
			}
		}

		public bool Remove(ServiceLine serviceLine)
		{
			try {
				if (_serviceLineList.ContainsKey(serviceLine.ServiceLineNumber))
					_serviceLineList.Remove(serviceLine.ServiceLineNumber);
				else
					_logger.LogWarning("ServiceLine {serviceLineNumber} does not exist to be removed.", serviceLine.ServiceLineNumber);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing serviceLine. {serviceLine}. Error: {errorMessage}", serviceLine, _Ex.Message);

				return false;
			}
		}

		public bool Remove(string ServiceLineNumber)
		{
			try {
				if (_serviceLineList.ContainsKey(ServiceLineNumber))
					_serviceLineList.Remove(ServiceLineNumber);
				else
					_logger.LogWarning("ServiceLine {serviceLineNumber} does not exist to be removed.", ServiceLineNumber);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing serviceLine. {serviceLineNumber}. Error: {errorMessage}", ServiceLineNumber, _Ex.Message);

				return false;
			}
		}

		public ServiceLine Get (string ServiceLineNumber)
		{
			if (_serviceLineList.ContainsKey(ServiceLineNumber))
				return _serviceLineList[ServiceLineNumber];
			else
				return null;
		}

		IEnumerator<ServiceLine> IEnumerable<ServiceLine>.GetEnumerator()
		{
			return _serviceLineList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _serviceLineList.GetEnumerator();
		}
	}
}
