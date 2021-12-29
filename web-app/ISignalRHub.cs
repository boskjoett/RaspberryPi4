namespace PiWebApp
{
    public interface ISignalRHub
    {
        Task SendButtonState(bool pressed);
    }
}
