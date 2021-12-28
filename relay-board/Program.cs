using System;
using System.Threading;
using System.Device.Gpio;

namespace RelayBoard
{
    //
    // This program controls the relays of a RPi Relay Board
    // See: https://www.waveshare.com/rpi-relay-board.htm
    //
    class Program
    {
        static void Main(string[] args)
        {
            const int Relay1Pin = 26;
            const int Relay2Pin = 20;
            const int Relay3Pin = 21;
            
            Console.WriteLine($"Toggling relay states");

            using (GpioController controller = new GpioController())
            {
                controller.OpenPin(Relay1Pin, PinMode.Output);
                controller.OpenPin(Relay2Pin, PinMode.Output);
                controller.OpenPin(Relay3Pin, PinMode.Output);

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    Console.WriteLine($"Setting relay 1 ON");
                    controller.Write(Relay1Pin, PinValue.High);
                    Thread.Sleep(3000);

                    Console.WriteLine($"Setting relay 1 OFF");
                    controller.Write(Relay1Pin, PinValue.Low);
                    Thread.Sleep(3000);
                }
            }
        }
    }
}
