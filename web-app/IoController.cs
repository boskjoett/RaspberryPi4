using PiWebApp.Models;
using System.Device.Gpio;

namespace PiWebApp
{
    /// <summary>
    /// Controls the I/O pins on a Raspberry Pi mounted with an RPi Relay Board.
    /// </summary>
    /// <see href="https://www.waveshare.com/rpi-relay-board.htm"/>
    public class IoController : IIoController
    {
        #region I/O Pin Definitions
        private const int Relay1Pin = 26;
        private const int Relay2Pin = 20;
        private const int Relay3Pin = 21;
        #endregion

        private readonly GpioController _controller;

        public IoController()
        {
            _controller = new GpioController();
            _controller.OpenPin(Relay1Pin, PinMode.Output);
            _controller.OpenPin(Relay2Pin, PinMode.Output);
            _controller.OpenPin(Relay3Pin, PinMode.Output);
        }

        public void Dispose()
        {
            _controller.Dispose();
        }

        public void SetRelayState(Relay relay, bool on)
        {
            int pinNumber;

            switch (relay)
            {
                case Relay.Relay1:
                    pinNumber = Relay1Pin;
                    break;
                case Relay.Relay2:
                    pinNumber = Relay2Pin;
                    break;
                case Relay.Relay3:
                    pinNumber = Relay3Pin;
                    break;
                default:
                    return;
            }

            _controller.Write(pinNumber, on ? PinValue.High : PinValue.Low);
        }
    }
}
