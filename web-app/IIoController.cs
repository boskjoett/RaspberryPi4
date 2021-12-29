using PiWebApp.Models;

namespace PiWebApp
{
    public interface IIoController : IDisposable
    {
        event EventHandler<EventArgs>? ButtonPressed;
        event EventHandler<EventArgs>? ButtonReleased;

        bool IsButtonPressed { get; }

        void SetRelayState(Relay relay, bool on);

        void SetLedState(bool on);
    }
}
