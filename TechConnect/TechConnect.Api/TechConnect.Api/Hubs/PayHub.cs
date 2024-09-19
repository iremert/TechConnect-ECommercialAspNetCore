using Microsoft.AspNetCore.SignalR;

namespace TechConnect.Api.Hubs
{
    public class PayHub:Hub
    {
        public async Task SendPaymentStatus(string status)
        {
            await Clients.All.SendAsync("Receive", status);
        }
    }
}
