// File Name: AccessTokenResponseModel.cs
// Author: rameshvishnubhatla
// Date Created: 7/24/2023
//
//
using System;
namespace StarlinkDC.Models.Starlink
{
	public class AccessTokenResponseModel
	{
		public string Access_token { get; set; }
		public int Expires_in { get; set; }
		public string Token_type { get; set; }
		public string Scope { get; set; }
	}
}

