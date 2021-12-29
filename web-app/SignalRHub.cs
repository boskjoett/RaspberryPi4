using Microsoft.AspNetCore.SignalR;

namespace PiWebApp
{
    public class SignalRHub : Hub, ISignalRHub
    {
        private readonly ILogger<SignalRHub> _logger;

        public SignalRHub(ILogger<SignalRHub> logger)
        {
            _logger = logger;
        }

        public async Task SendButtonStateAsync(bool pressed)
        {
            _logger.LogInformation($"Sending button state {pressed} over SignalR");
            await Clients.All.SendAsync("ButtonStateChanged", pressed);
        }
    }
}
