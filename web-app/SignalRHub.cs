using Microsoft.AspNetCore.SignalR;

namespace PiWebApp
{
    public class SignalRHub : Hub
    {
        public async Task SendButtonStateAsync(bool pressed)
        {
            await Clients.All.SendAsync("ButtonState", pressed);
        }
    }
}
