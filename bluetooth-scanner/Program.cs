using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using HashtagChris.DotNetBlueZ;
using HashtagChris.DotNetBlueZ.Extensions;

namespace BluetoothScanner
{
    // A simple program that scans for Bluetooth devices.
    //
    // .NET Core Bluetooth support is provided by this NuGet package:
    // https://www.nuget.org/packages/HashtagChris.DotNetBlueZ
    // See samples at:
    // https://github.com/hashtagchris/DotNet-BlueZ
    //
    // The BlueZ stack must be installed on the Pi.
    //   sudo apt install bluez
    class Program
    {
        private const int SecondsToScan = 15;
        private static TimeSpan timeout = TimeSpan.FromSeconds(15);

        static async Task Main(string[] args)
        {
            IAdapter1 adapter;
            var adapters = await BlueZManager.GetAdaptersAsync();
            if (adapters.Count == 0)
            {
                Console.WriteLine("No Bluetooth adapters found");
                return;
            }

            adapter = adapters.First();
            var adapterPath = adapter.ObjectPath.ToString();
            var adapterName = adapterPath.Substring(adapterPath.LastIndexOf("/") + 1);
            Console.WriteLine($"Using Bluetooth adapter {adapterName}");

            // Print out the devices we already know about.
            var devices = await adapter.GetDevicesAsync();
            foreach (var device in devices)
            {
                string deviceDescription = await GetDeviceDescriptionAsync(device);
                Console.WriteLine(deviceDescription);
            }

            Console.WriteLine($"{devices.Count} device(s) found ahead of scan.");
            Console.WriteLine();

            // Scan for more devices.
            Console.WriteLine($"Scanning for {SecondsToScan} seconds...");

            int newDevices = 0;
            using (await adapter.WatchDevicesAddedAsync(async device => {
                newDevices++;
                // Write a message when we detect new devices during the scan.
                string deviceDescription = await GetDeviceDescriptionAsync(device);
                Console.WriteLine($"[NEW] {deviceDescription}");

//                await PrintDeviceInformation(device);
            }))
            {
                await adapter.StartDiscoveryAsync();
                await Task.Delay(TimeSpan.FromSeconds(SecondsToScan));
                await adapter.StopDiscoveryAsync();
            }

            Console.WriteLine($"Scan complete. {newDevices} new device(s) found");
        }


        private static async Task<string> GetDeviceDescriptionAsync(IDevice1 device)
        {
            var deviceProperties = await device.GetAllAsync();
            return $"{deviceProperties.Alias} (Address: {deviceProperties.Address}, RSSI: {deviceProperties.RSSI})";
        }

        private static async Task PrintDeviceInformation(IDevice1 device)
        {
            var service = await device.GetServiceAsync(GattConstants.DeviceInformationServiceUUID);
            var modelNameCharacteristic = await service.GetCharacteristicAsync(GattConstants.ModelNameCharacteristicUUID);
            var manufacturerCharacteristic = await service.GetCharacteristicAsync(GattConstants.ManufacturerNameCharacteristicUUID);

            Console.WriteLine("Reading Device Info characteristic values...");
            var modelNameBytes = await modelNameCharacteristic.ReadValueAsync(timeout);
            var manufacturerBytes = await manufacturerCharacteristic.ReadValueAsync(timeout);

            Console.WriteLine($"Model name: {Encoding.UTF8.GetString(modelNameBytes)}");
            Console.WriteLine($"Manufacturer: {Encoding.UTF8.GetString(manufacturerBytes)}");            
        }
    }
}
