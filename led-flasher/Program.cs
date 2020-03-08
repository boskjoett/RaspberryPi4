using System;
using System.Threading;
using System.Device.Gpio;

namespace LedFlasher
{
    class Program
    {
        static void Main(string[] args)
        {
            int pin = 17;
            int lightTimeInMilliseconds = 1000;
            int dimTimeInMilliseconds = 500;
            
            Console.WriteLine($"Let's blink an LED!");

            using (GpioController controller = new GpioController())
            {
                controller.OpenPin(pin, PinMode.Output);
                Console.WriteLine($"GPIO pin enabled for use: {pin}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    Console.WriteLine($"Light for {lightTimeInMilliseconds}ms");
                    controller.Write(pin, PinValue.High);
                    Thread.Sleep(lightTimeInMilliseconds);

                    Console.WriteLine($"Dim for {dimTimeInMilliseconds}ms");
                    controller.Write(pin, PinValue.Low);
                    Thread.Sleep(dimTimeInMilliseconds);
                }
            }
        }
    }
}
