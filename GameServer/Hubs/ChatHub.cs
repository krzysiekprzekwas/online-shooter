using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}