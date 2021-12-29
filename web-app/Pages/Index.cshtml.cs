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

            _ioController.ButtonPressed += OnButtonPressed;
            _ioController.ButtonReleased += OnButtonReleased;
        }

        public void OnGet()
        {
            ButtonPressed = _ioController.IsButtonPressed;
            _logger.LogInformation($"Current button state is {(ButtonPressed ? "pressed" : "released")}");
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


        private void OnButtonPressed(object? sender, EventArgs e)
        {
            _logger.LogInformation("Button is pressed");
        }

        private void OnButtonReleased(object? sender, EventArgs e)
        {
            _logger.LogInformation("Button is released");
        }
    }
}