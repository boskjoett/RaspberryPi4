using Microsoft.AspNetCore.Mvc.RazorPages;
using PiWebApp.Models;

namespace PiWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIoController _ioController;
        private readonly ILogger<IndexModel> _logger;

        public bool ButtonPressed { get; private set; }

        public IndexModel(IIoController ioController, ILogger<IndexModel> logger)
        {
            _ioController = ioController;
            _logger = logger;
        }

        public void OnGet()
        {
            ButtonPressed = _ioController.ReadButtonState();
        }

        public void OnPostRelay(int relay, bool on)
        {
            _logger.LogInformation($"Setting relay {relay} {(on ? "on" : "off")}");
            _ioController.SetRelayState((Relay)relay, on);
        }

        public void OnPostLed(bool on)
        {
            _logger.LogInformation($"Setting LED {(on ? "on" : "off")}");
            _ioController.SetLedState(on);
        }
    }
}