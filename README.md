# Raspberry Pi 4 Stuff
This repo contains Raspberry Pi 4 projects written in C# using .NET Core.

| Folder | Content |
|--------|---------|
| led-flasher       | Simple program that flashes an LED. |
| button-poller     | Simple program that turns on an LED and a buzzer while a button is pressed. |
| bluetooth-scanner | Simple program that scans for Bluetooth devices. Uses the BlueZ Bluetooth stack. |
| google-assistant  | Google Assistant demo |

These projects can run both on Raspian and Ubuntu for Raspberry Pi.

![Photo](photo.jpg)

## GPIO Ports

GPIO connector pinout on a Raspberry Pi 4

![Pinout](Pi4_GPIO.png)

See also 
* https://www.raspberrypi.org/documentation/usage/gpio/
* https://darenmay.com/blog/net-core-and-gpio-on-the-raspberry-pi---leds-and-gpio/
* https://www.hanselman.com/blog/InstallingTheNETCore2xSDKOnARaspberryPiAndBlinkingAnLEDWithSystemDeviceGpio.aspx

**IMPORTANT**<br/>
You must be root to control GPIO pins.

## Bluetooth
* http://www.bluez.org/ - BlueZ - The official Linux Bluetooth protocol stack.
* https://www.nuget.org/packages/HashtagChris.DotNetBlueZ - .NET Core NuGet package using BlueZ.

    sudo apt install bluez

## Webcams

Links regarding how to use standard USB webcams with a Raspberry Pi:

* https://www.raspberrypi.org/documentation/usage/webcams/
* https://motion-project.github.io/

## Audio

Audio can be played either on HDMI or the 3.5 mm jack plug (default).

To enable audio on Ubuntu:

Insert this line in /boot/firmware/usercfg.txt:<br />
dtparam=audio=on

To list available sound playback (speaker) devices:

    sudo aplay -L
or 

    sudo aplay -l

To list available sound recording (microphone) devices:

    sudo arecord -L

or 

    sudo arecord -l

Audio can be tested by the **speaker-test** command.

    sudo speaker-test -t wav

.wav files can be played with **aplay**

To test audio on a Jabra SPEAK 410 USB speakerphone:

    sudo speaker-test -Dplughw:USB -t wav

### MP3

omxplayer-pi is not available on Ubuntu ARM64

MP3 files can be played with **mpg321**

To install mpg321

    sudo apt install mpg321

## Google Assistant

See https://pimylifeup.com/raspberry-pi-google-assistant/
