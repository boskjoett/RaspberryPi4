using System;
using System.Threading;
using System.Device.Gpio;

namespace LedFlasher
{
    //
    // This program assumes that a DHT11 temperature and humidity sensor
    // has its data pin connected to GPIO pin 18.
    //
    class Program
    {
        static void Main(string[] args)
        {
            int pin = 18;
            int temperature = 0;
            int humidity = 0;
            bool tryagain = true;
            
            Console.WriteLine("Reading temperature and humidity");

            using (GpioController controller = new GpioController())
            {
                do
                {
                    Console.Write("Press 'q' to quit. Any other key to continue...");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Q)
                        return;

                    Console.WriteLine("Sending start pulse to sensor");

                    // Set pin in output mode
                    controller.OpenPin(pin, PinMode.Output);

                    // Write start pulse by setting pin 
                    // high, then low for minimum 18 ms, then high.
                    controller.Write(pin, PinValue.High);
                    Thread.Sleep(100);

                    controller.Write(pin, PinValue.Low);
                    Thread.Sleep(50);
                    controller.Write(pin, PinValue.High);

                    // Set pin in input mode
                    controller.SetPinMode(pin, PinMode.Input);

                    // After a start pulse the sensor sends a response pulse where input goes from high to low.
                    // Detect the transition from high to low.

                    int loopCounter = 0;

                    while (controller.Read(pin) == PinValue.High && loopCounter < 1000000)
                    {
                        loopCounter++;
                    }

                    if (loopCounter >= 1000000)
                    {
                        Console.WriteLine("Response high to low transition not detected");
                        continue;
                    }

                    loopCounter = 0;
                    while (controller.Read(pin) == PinValue.Low && loopCounter < 100000)
                    {
                        loopCounter++;
                    }

                    if (loopCounter >= 100000)
                    {
                        Console.WriteLine("Response low to high transition not detected");
                        continue;
                    }

                    // Read 5 bytes of data from sensor
                    // Each byte contains 8 bits.
                    // First two bytes contains the humidity.
                    // Next two bytes contains the temperature.
                    // 5th byte is a checksum of the previous 4 bytes. The checksum is the unsigned sum of the first 4 bytes.

                    byte[] data = new byte[5];

                    for (int bytesRead = 0; bytesRead < 5; bytesRead++)
                    {
                        // Each data bit starts with a high to low transition that stays low for 54 microseconds
                        loopCounter = 0;

                        while (controller.Read(pin) == PinValue.High && loopCounter < 100000)
                        {
                            loopCounter++;
                        }

                        if (loopCounter >= 100000)
                        {
                            Console.WriteLine("Data high to low transition not detected");
                            continue;
                        }

                        // Detect low to high transition. The duration of the high state determines the bit value
                        loopCounter = 0;

                        while (controller.Read(pin) == PinValue.Low && loopCounter < 100000)
                        {
                            loopCounter++;
                        }

                        if (loopCounter >= 100000)
                        {
                            Console.WriteLine("Data low to high transition not detected");
                            continue;
                        }
                    }

                    Console.WriteLine($"Temperature: {temperature}.  Humidity: {humidity}%");

                } while (tryagain);
            }
        }
    }
}
