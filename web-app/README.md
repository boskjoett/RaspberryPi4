# Description

ASP.NET 6.0 web app for controlling I/O pins on a [Raspberry Pi 4 model B](https://www.raspberrypi.com/products/raspberry-pi-4-model-b/)
with a [RPi Relay Board](https://www.waveshare.com/wiki/RPi_Relay_Board) mounted.

### I/O pin usage

| Pin   | Mode           | Use     |
|------ |----------------|---------|
| 26    | Output         | Relay 1 |
| 20    | Output         | Relay 2 |
| 21    | Output         | Relay 3 |
| 17    | Output         | LED     |
| 22    | Input pulldown | Button  |

### Running the code

1. Install .NET on the Raspberry Pi.

2. Copy the source code to the Raspberry Pi or clone it with git.

3. Run the code as *root** with this command <br />``dotnet run -c Release --urls "http://0.0.0.0:80"``

Remember to open port 80 in the firewall on Raspberry Pi with this command<br />
``sudo ufw allow 80/tcp``

