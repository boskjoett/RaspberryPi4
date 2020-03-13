using System;
using System.Threading;
using System.Device.Gpio;

namespace ButtonPoller
{
    //
    // This program requires that a normally-open push button is connected between GPIO 22 (pin 15) and 3.3V on pin 17.
    // with a 470 Ohms resistor in series.
    // It also requires that an LED is connected between GPIO 17 (pin 11) and GND (pin 9)
    // with a 390 Ohms resistor in series.
    //
    class Program
    {
        static void Main(string[] args)
        {
            int ledPin = 17;
            int buttonPin = 22;
            
            Console.WriteLine("Press the button to turn on the LED");

            using (GpioController controller = new GpioController())
            {
                controller.OpenPin(ledPin, PinMode.Output);
                Console.WriteLine($"LED GPIO pin set to output mode: {ledPin}");

                controller.OpenPin(buttonPin, PinMode.InputPullDown);
                Console.WriteLine($"Button GPIO pin set to input mode: {buttonPin}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    if (controller.Read(buttonPin) == PinValue.High)
                    {
                        // Button is pressed, turn on LED
                        controller.Write(ledPin, PinValue.High);
                    }
                    else
                    {
                        // Turn LED off
                        controller.Write(ledPin, PinValue.Low);
                    }

                    Thread.Sleep(200);
                }
            }
        }
    }
}
