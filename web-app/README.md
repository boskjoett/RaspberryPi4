# Description

ASP.NET 6.0 web app for controlling I/O pins on a [Raspberry Pi 4 model B](https://www.raspberrypi.com/products/raspberry-pi-4-model-b/)
with a [RPi Relay Board](https://www.waveshare.com/wiki/RPi_Relay_Board) mounted.

It can also turn an LED on/off and detect the state of a pushbutton.

The state of the button is pushed to the browser on changes using SignalR.

The web app uses Razor pages and [Bootstrap](https://getbootstrap.com/) for web components and responsive layout.

### I/O pin usage

| GPIO | Mode           | Use     |
|----- |----------------|---------|
| 26   | Output         | Relay 1 |
| 20   | Output         | Relay 2 |
| 21   | Output         | Relay 3 |
| 17   | Output         | LED anode. Katode is connected to GND on pin 9 |
| 22   | Input pulldown | Button. Other button terminal is connected to 3.3 V power on pin 17 |

### Running the code

1. Install .NET on the Raspberry Pi.

2. Copy the source code to the Raspberry Pi or clone it with git.

3. Run the code as *root** with this command <br />``dotnet run -c Release --urls "http://0.0.0.0:80"``

Remember to open port 80 in the firewall on Raspberry Pi with this command<br />
``sudo ufw allow 80/tcp``

