using System;
using System.Threading;
using System.Device.Gpio;

namespace ButtonPoller
{
    //
    // This program requires that a normally-open push button is connected between GPIO 22 (pin 15) and 3.3V on pin 17.
    // with a 470 Ohms resistor in series.
    // It assumes that an LED is connected between GPIO 17 (pin 11) and GND (pin 9)
    // with a 390 Ohms resistor in series.
    // It assumes that a piezo buzzer is connected between GPIO 26 (pin 37) and GND (pin 39).
    class Program
    {
        static void Main(string[] args)
        {
            int ledPin = 17;
            int buttonPin = 22;
            int buzzerPin = 26;
            
            Console.WriteLine("Press the button to turn on the LED and buzz");

            using (GpioController controller = new GpioController())
            {
                controller.OpenPin(ledPin, PinMode.Output);
                Console.WriteLine($"LED GPIO pin {ledPin} set to output mode");

                controller.OpenPin(buttonPin, PinMode.InputPullDown);
                Console.WriteLine($"Button GPIO pin {buttonPin} set to input mode");

                controller.OpenPin(buzzerPin, PinMode.Output);
                Console.WriteLine($"Buzzer GPIO pin {buzzerPin} set to output mode");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    if (controller.Read(buttonPin) == PinValue.High)
                    {
                        // Button is pressed
                        // Turn on LED
                        controller.Write(ledPin, PinValue.High);

                        // Turn on buzzer
                        controller.Write(buzzerPin, PinValue.High);

                    }
                    else
                    {
                        // Turn LED off
                        controller.Write(ledPin, PinValue.Low);

                        // Turn buzzer off
                        controller.Write(buzzerPin, PinValue.Low);
                    }

                    Thread.Sleep(200);
                }
            }
        }
    }
}
