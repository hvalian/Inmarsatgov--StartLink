// File Name: UserTerminals.cs
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
	public class UserTerminals : IEnumerable<UserTerminal>
	{
		private readonly ILogger _logger;
		private Dictionary<string, UserTerminal> _userTerminalList = new Dictionary<string, UserTerminal>();

		public UserTerminals(ILogger Logger)
		{
			_logger = Logger;
		}

		public bool Clear()
		{
			try {
				_userTerminalList.Clear();
				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error clearing user terminal list dictionary. Error: {errorMessage}", _Ex.Message);
				return false;
			}
		}

		public bool Add(UserTerminal userTerminal)
		{
			_logger.LogDebug("Entering Add(UserTerminal) with argument {userTerminal}.", userTerminal);
			try {
                if (userTerminal.Active==false && userTerminal.DeactivationDate == null)
				{
					userTerminal.DeactivationDate = Helper.Utility.GetDateTime();
				}
				else if (userTerminal.Active == true && userTerminal.DeactivationDate != null)
                {
					userTerminal.DeactivationDate = null;
                }

                string key = Helper.Terminals.GetKey(userTerminal);

                if (_userTerminalList.ContainsKey(key)) {
					_logger.LogDebug("UserTerminal {userTerminalId} already exists.  Replacing it with new data.", key);
					_userTerminalList[key] = userTerminal;
					return true;
				}

				_userTerminalList.Add(key, userTerminal);

				_logger.LogDebug("UserTerminal {userTerminal} added successfully.", userTerminal);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding userTerminal. {userTerminal}. Error: {errorMessage}", userTerminal, _Ex.Message);

				return false;
			}
		}

        public bool Remove(UserTerminal userTerminal)
        {
            string key = Helper.Terminals.GetKey(userTerminal);

            try {
                if (_userTerminalList.ContainsKey(key))
                    _userTerminalList.Remove(key);
                else
                    _logger.LogWarning("UserTerminal {userTerminalId} does not exist to be removed.", key);

                return true;
            } catch (Exception _Ex) {
                _logger.LogError("Error removing UserTerminal. {userTerminal}. Error: {errorMessage}", userTerminal, _Ex.Message);

                return false;
            }
        }

        public UserTerminal Get (UserTerminal userTerminal)
		{
            string key = Helper.Terminals.GetKey(userTerminal);


            if (_userTerminalList.ContainsKey(key))
				return _userTerminalList[key];
			else
				return null;
		}

		public UserTerminal Get(string UserTerminalId)
        {
			UserTerminal selectedTerminal = null;

            foreach (var s in _userTerminalList.Where(kv => kv.Key.StartsWith(UserTerminalId)))
			{
                if (_userTerminalList.ContainsKey(s.Key))
				{
                    UserTerminal  item = _userTerminalList[s.Key];
					if (item.Active)
					{
						return item;
					}
					else
					{
						if (selectedTerminal == null || (
							selectedTerminal != null  && 
							selectedTerminal.DeactivationDate != null && 
							item.DeactivationDate != null && 
							selectedTerminal.DeactivationDate < item.DeactivationDate))
						{ 
							selectedTerminal = item;
						}
					}
                }
            }

            return selectedTerminal;
		}

		IEnumerator<UserTerminal> IEnumerable<UserTerminal>.GetEnumerator()
		{
			return _userTerminalList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _userTerminalList.GetEnumerator();
		}
	}
}
