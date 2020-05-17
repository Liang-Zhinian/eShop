// Author: 	sprite
// On:		2020/5/14
using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PaymentServiceProvider.Hubs
{
    public class PaymentHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
