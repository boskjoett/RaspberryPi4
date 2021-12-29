using PiWebApp.Models;

namespace PiWebApp
{
    public interface IIoController : IDisposable
    {
        event EventHandler<EventArgs>? ButtonPressed;
        event EventHandler<EventArgs>? ButtonReleased;

        void SetRelayState(Relay relay, bool on);

        void SetLedState(bool on);

        bool ReadButtonState();
    }
}
