# RaspberryPi4
This repo contains Raspberry Pi 4 projects written in C# using .NET Core.

| Folder | Content |
|--------|---------|
| led-flasher       | Simple program that flashes an LED. |
| input-poller      | Simple program that detects if a button is pressed. |
| bluetooth-scanner | Simple program that scans for Bluetooth devices. Uses the BlueZ Bluetooth stack. |

![Photo](photo.jpg)

## GPIO Ports

GPIO connector pinout on a Raspberry Pi 4

![Pinout](Pi4_GPIO.png)

## Bluetooth
* http://www.bluez.org/ - BlueZ - The official Linux Bluetooth protocol stack.
* https://www.nuget.org/packages/HashtagChris.DotNetBlueZ - .NET Core NuGet package using BlueZ.

    sudo apt install bluez

## Links
* https://darenmay.com/blog/net-core-and-gpio-on-the-raspberry-pi---leds-and-gpio/
* https://www.raspberrypi.org/documentation/usage/gpio/
* https://www.hanselman.com/blog/InstallingTheNETCore2xSDKOnARaspberryPiAndBlinkingAnLEDWithSystemDeviceGpio.aspx
