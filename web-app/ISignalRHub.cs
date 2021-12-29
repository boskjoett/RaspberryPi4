namespace PiWebApp
{
    public interface ISignalRHub
    {
        Task SendButtonStateAsync(bool pressed);
    }
}
