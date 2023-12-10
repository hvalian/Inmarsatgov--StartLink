// File Name: Accounts.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System;
using System.Collections;
using System.Security.Principal;
using StarlinkModels = IG.ENMS.Starlink.Models;

namespace IG.ENMS.Starlink.Data
{
	public class Accounts : IEnumerable<StarlinkModels.Account>
	{
		private readonly ILogger _logger;
		public Dictionary<string, StarlinkModels.Account> _accountList = new Dictionary<string, StarlinkModels.Account>();

		public Accounts(ILogger Logger)
		{
			_logger = Logger;
		}

		public bool Clear()
		{
			try {
				_accountList.Clear();
				return true;
			}
			catch (Exception _Ex) {
				_logger.LogError("Error clearing account list dictionary. Error: {errorMessage}", _Ex.Message);
				return false;
			}
		}

		public bool Add(StarlinkModels.Account account)
		{
			_logger.LogDebug("Entering Add(Account) with argument {account}.", account);
			try {
				if (_accountList.ContainsKey(account.AccountNumber)) {
					_logger.LogDebug("Account {accountNumber} already exists.  Replacing it with new data.", account.AccountNumber);
					_accountList[account.AccountNumber] = account;

					return true;
				}

				_accountList.Add(account.AccountNumber, account);

				_logger.LogDebug("Account {account} added successfully.", account);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding account. {account}. Error: {errorMessage}", account, _Ex.Message);

				return false;
			}
		}

		public bool Add(string AccountNumber, string RegionCode, string AccountName)
		{
			StarlinkModels.Account account;

			_logger.LogDebug("Entering Add(Account) with arguments AccountNumber:{accountNumber}, RegionCode:{regionCode}, AccountName:{accountName}.", AccountNumber, RegionCode, AccountNumber);

			try {
				if (_accountList.ContainsKey(AccountNumber)) {
					_logger.LogDebug("Account {accountNumber} already exists.  Replacing it with new data.", AccountNumber);

					account = new StarlinkModels.Account() { AccountNumber = AccountNumber, RegionCode = RegionCode, AccountName = AccountName };

					_accountList[AccountNumber] = account;

					return true;
				}

				account = new StarlinkModels.Account() { AccountNumber = AccountNumber, RegionCode = RegionCode, AccountName = AccountName };

				_accountList.Add(AccountNumber, account);

				_logger.LogDebug("Account for AccountNumber:{accountNumber}, RegionCode:{regionCode}, AccountName:{accountName} added successfully.", AccountNumber, RegionCode, AccountNumber);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding account with arguments AccountNumber:{accountNumber}, RegionCode:{regionCode}, AccountName:{accountName}. Error: {errorMessage}", AccountNumber, RegionCode, AccountNumber, _Ex.Message);

				return false;
			}
		}

		public bool Remove(StarlinkModels.Account account)
		{
			try {
				if (_accountList.ContainsKey(account.AccountNumber))
					_accountList.Remove(account.AccountNumber);
				else
					_logger.LogWarning("Account {accountNumber} does not exist to be removed.", account.AccountNumber);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing account. {account}. Error: {errorMessage}", account, _Ex.Message);

				return false;
			}
		}

		public bool Remove(string AccountNumber)
		{
			try {
				if (_accountList.ContainsKey(AccountNumber))
					_accountList.Remove(AccountNumber);
				else
					_logger.LogWarning("Account {accountNumber} does not exist to be removed.", AccountNumber);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing account. {accountNumber}. Error: {errorMessage}", AccountNumber, _Ex.Message);

				return false;
			}
		}

		public StarlinkModels.Account Get (string AccountNumber)
		{
			if (_accountList.ContainsKey(AccountNumber))
				return _accountList[AccountNumber];
			else
				return null;
		}

		IEnumerator<StarlinkModels.Account> IEnumerable<StarlinkModels.Account>.GetEnumerator()
		{
			return _accountList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _accountList.GetEnumerator();
		}
	}
}
