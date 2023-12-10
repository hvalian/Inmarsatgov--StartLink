// File Name: Log.cs
// Author: rameshvishnubhatla
// Date Created: 8/28/2023
//
//
using System;
using Serilog.Core;
using Serilog.Events;

namespace IG.ENMS.Starlink.Services
{
	public class AppLog : ILogEventSink
	{
		public AppLog()
		{
		}

		public void Emit(LogEvent logEvent)
		{
			//TODO: Write to log hub and to a database table
		}
	}
}

