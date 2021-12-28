using Microsoft.AspNetCore.Mvc.RazorPages;
using PiWebApp.Models;

namespace PiWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIoController _ioController;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IIoController ioController, ILogger<IndexModel> logger)
        {
            _ioController = ioController;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public void OnPost(int relay, bool on)
        {
            _logger.LogInformation($"Setting relay {relay} {(on ? "ON" : "OFF")}");
            _ioController.SetRelayState((Relay)relay, on);
        }
    }
}