using Microsoft.AspNetCore.SignalR;

namespace PiWebApp
{
    public class SignalRHub : Hub
    {
        private readonly ILogger<SignalRHub> _logger;

        public SignalRHub(ILogger<SignalRHub> logger)
        {
            _logger = logger;
        }

        public async Task SendButtonStateAsync(bool pressed)
        {
            try
            {
                _logger.LogInformation($"Sending button state {pressed} over SignalR");
                await Clients.All.SendAsync("ButtonStateChanged", pressed);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception sending button state: {ex.Message}");
            }
        }
    }
}
