// File Name: Addresses.cs
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
	public class Addresses : IEnumerable<Address>
	{
		private readonly ILogger _logger;
		private Dictionary<string, Address> _addressList = new Dictionary<string, Address>();

		public Addresses(ILogger Logger)
		{
			_logger = Logger;
		}

		public bool Clear()
		{
			try {
				_addressList.Clear();
				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error clearing address list dictionary. Error: {errorMessage}", _Ex.Message);
				return false;
			}
		}

		public bool Add(Address address)
		{
			_logger.LogDebug("Entering Add(Address) with argument {address}.", address);
			try {
				if (_addressList.ContainsKey(address.AddressReferenceId)) {
					_logger.LogDebug("Address {addressReferenceId} already exists.  Replacing it with new data.", address.AddressReferenceId);
					_addressList[address.AddressReferenceId] = address;
					return true;
				}

				_addressList.Add(address.AddressReferenceId, address);

				_logger.LogDebug("Address {address} added successfully.", address);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding address. {address}. Error: {errorMessage}", address, _Ex.Message);

				return false;
			}
		}

		public bool Add(string AddressReferenceId, List<string> AddressLines, string Locality, string AdministrativeArea, string AdministrativeAreaCode, string Region, string PostalCode, string Metadata, string FormattedAddress, double Latitude, double Longitude)
		{
			_logger.LogDebug("Entering Add(Address) with arguments  AddressReferenceId: {AddressReferenceId}, AddressLines:{AddressLines}, Locality: {Locality}, AdministrativeArea: {AdministrativeArea}, AdministrativeAreaCode: {AdministrativeAreaCode}, Region: {Region}, PostalCode: {PostalCode},Metadata: {Metadata},FormattedAddress: {FormattedAddress}, Latitude: {Latitude}, Longitude: {Longitude}.", AddressReferenceId, AddressLines, Locality, AdministrativeArea, AdministrativeAreaCode, Region, PostalCode, Metadata, FormattedAddress, Latitude, Longitude);

			try {
				if (_addressList.ContainsKey(AddressReferenceId)) {
					_logger.LogDebug("Address {AddressReferenceId} already exists.  Replacing it with new data.", AddressReferenceId);

					_addressList[AddressReferenceId] = new Address() {
						AddressReferenceId = AddressReferenceId, AddressLines = AddressLines, Locality = Locality, AdministrativeArea = AdministrativeArea, AdministrativeAreaCode = AdministrativeAreaCode, Region = Region, PostalCode = PostalCode, Metadata = Metadata, FormattedAddress = FormattedAddress, Latitude = Latitude, Longitude = Longitude
					};
					return true;
				}

				_addressList.Add(AddressReferenceId, new Address() {
					AddressReferenceId = AddressReferenceId, AddressLines = AddressLines, Locality = Locality, AdministrativeArea = AdministrativeArea, AdministrativeAreaCode = AdministrativeAreaCode, Region = Region, PostalCode = PostalCode, Metadata = Metadata, FormattedAddress = FormattedAddress, Latitude = Latitude, Longitude = Longitude
				});

				_logger.LogDebug("Address for AddressReferenceId: {AddressReferenceId}, AddressLines:{AddressLines}, Locality: {Locality}, AdministrativeArea: {AdministrativeArea}, AdministrativeAreaCode: {AdministrativeAreaCode}, Region: {Region}, PostalCode: {PostalCode},Metadata: {Metadata},FormattedAddress: {FormattedAddress}, Latitude: {Latitude}, Longitude: {Longitude} added successfully.", AddressReferenceId, AddressLines, Locality, AdministrativeArea, AdministrativeAreaCode, Region, PostalCode, Metadata, FormattedAddress, Latitude, Longitude);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding address with arguments AddressReferenceId: {AddressReferenceId}, AddressLines:{AddressLines}, Locality: {Locality}, AdministrativeArea: {AdministrativeArea}, AdministrativeAreaCode: {AdministrativeAreaCode}, Region: {Region}, PostalCode: {PostalCode},Metadata: {Metadata},FormattedAddress: {FormattedAddress}, Latitude: {Latitude}, Longitude: {Longitude}. Error {errorMessage}", AddressReferenceId, AddressLines, Locality, AdministrativeArea, AdministrativeAreaCode, Region, PostalCode, Metadata, FormattedAddress, Latitude, Longitude, _Ex.Message);

				return false;
			}
		}

		public bool Remove(Address address)
		{
			try {
				if (_addressList.ContainsKey(address.AddressReferenceId))
					_addressList.Remove(address.AddressReferenceId);
				else
					_logger.LogWarning("Address {addressNumber} does not exist to be removed.", address.AddressReferenceId);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing address. {address}. Error: {errorMessage}", address, _Ex.Message);

				return false;
			}
		}

		public bool Remove(string AddressReferenceId)
		{
			try {
				if (_addressList.ContainsKey(AddressReferenceId))
					_addressList.Remove(AddressReferenceId);
				else
					_logger.LogWarning("Address {addressReferenceId} does not exist to be removed.", AddressReferenceId);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing address. {addressReferenceId}. Error: {errorMessage}", AddressReferenceId, _Ex.Message);

				return false;
			}
		}

		public Address Get (string AddressReferenceId)
		{
			if (_addressList.ContainsKey(AddressReferenceId))
				return _addressList[AddressReferenceId];
			else
				return null;
		}

		IEnumerator<Address> IEnumerable<Address>.GetEnumerator()
		{
			return _addressList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _addressList.GetEnumerator();
		}
	}
}
