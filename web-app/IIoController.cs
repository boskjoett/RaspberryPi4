using PiWebApp.Models;

namespace PiWebApp
{
    public interface IIoController : IDisposable
    {
        void SetRelayState(Relay relay, bool on);
    }
}
