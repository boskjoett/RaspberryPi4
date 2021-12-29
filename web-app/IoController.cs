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
        private const int ButtonPin = 22;
        private const int LedPin = 17;
        #endregion

        private readonly GpioController _controller;
        private CancellationTokenSource _cancellationTokenSource;

        public event EventHandler<EventArgs>? ButtonPressed;
        public event EventHandler<EventArgs>? ButtonReleased;

        public IoController()
        {
            _controller = new GpioController();
            _controller.OpenPin(Relay1Pin, PinMode.Output);
            _controller.OpenPin(Relay2Pin, PinMode.Output);
            _controller.OpenPin(Relay3Pin, PinMode.Output);

            _controller.OpenPin(LedPin, PinMode.Output);
            _controller.OpenPin(ButtonPin, PinMode.InputPullDown);

            // Turn all relays off
            _controller.Write(Relay1Pin, PinValue.High);
            _controller.Write(Relay2Pin, PinValue.High);
            _controller.Write(Relay3Pin, PinValue.High);

            _cancellationTokenSource = new CancellationTokenSource();

            SubscribeToButtonEvents(ButtonPin);
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

            _controller.Write(pinNumber, on ? PinValue.Low : PinValue.High);
        }

        public void SetLedState(bool on)
        {
            _controller.Write(LedPin, on ? PinValue.High : PinValue.Low);
        }

        public bool ReadButtonState()
        {
            PinValue value = _controller.Read(ButtonPin);

            // The button is pressed when the input pin is low
            return value == PinValue.Low ? true : false;
        }

        public void SubscribeToButtonEvents(int pinNumber)
        {
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    WaitForEventResult result = _controller.WaitForEvent(pinNumber, PinEventTypes.Rising | PinEventTypes.Falling, _cancellationTokenSource.Token);
                    try
                    {
                        if (result.EventTypes == PinEventTypes.Falling)
                        {
                            ButtonPressed?.Invoke(this, EventArgs.Empty);
                        }
                        else if (result.EventTypes == PinEventTypes.Rising)
                        {
                            ButtonReleased?.Invoke(this, EventArgs.Empty);
                        }
                    }
                    catch
                    {
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _controller.Dispose();
        }
    }
}
