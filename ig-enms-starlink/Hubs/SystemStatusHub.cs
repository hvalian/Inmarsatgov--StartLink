// File Name: SystemStatus.cs
// Author: rameshvishnubhatla
// Date Created: 6/16/2023
//
//
using System;
using Microsoft.AspNetCore.SignalR;
namespace StarlinkDC.Hubs
{
	public class SystemStatusHub : Hub
	{
		public SystemStatusHub()
		{
		}

		public override Task OnConnectedAsync()
		{
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			return base.OnDisconnectedAsync(exception);
		}

		public async Task SendStatus(string statusMessage)
		{
			// Broadcast the received message to all connected clients
			await Clients.All.SendAsync("ReceiveStatus", statusMessage);
		}

		
	}
}

