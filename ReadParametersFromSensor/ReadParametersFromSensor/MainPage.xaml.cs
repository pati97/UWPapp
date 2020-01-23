using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReadParametersFromSensor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private BuildAzure.IoT.Adafruit.BME280.BME280Sensor bME280;
        private static DeviceClient DeviceClient = null;
        
        const float seaLevelPressure = 1022.00f;
        private static string connectionStringStandart = "HostName=IotHubWeather.azure-devices.net;DeviceId=MyRpi;SharedAccessKey=Xeh7mlp/FG+es6OYnkQgco3cWPnfdHnn5LHzF/U3wBk=";
        private static string connectionStringFree = "HostName=IoTHubMember.azure-devices.net;DeviceId=MyRpi;SharedAccessKey=F4CptKs4Pn9bc1oi43GXsqFtgsKAFnEd2q5XA53rKRk=";      
        private static string deviceID = "MyRpi";
        private int messageId = 1;
        private DateTime date;
        //private static string iotHubUri = "IoTHubMember.azure-devices.net";
        //private static string deviceId = "MyRpi";
        //private static string deviceKey = "F4CptKs4Pn9bc1oi43GXsqFtgsKAFnEd2q5XA53rKRk=";
        public MainPage()
        {
            this.InitializeComponent();
            DeviceClient = DeviceClient.CreateFromConnectionString(connectionStringStandart, TransportType.Amqp);
            bME280 = new BuildAzure.IoT.Adafruit.BME280.BME280Sensor();
            DeviceToCloudMessage();
        }

        private async void DeviceToCloudMessage()
        {
            await bME280.Initialize();
            double temp = 0.00d;
            float hum = 0.00f;
            float press = 0.00f;
            while (true)
            {
                temp = await bME280.ReadTemperature();
                hum = await bME280.ReadHumidity();
                press = await bME280.ReadPressure() / 100;

                var telemetryDataPoint = new
                {
                    messageId = messageId++,
                    deviceId = deviceID,
                    dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    temperature = temp,
                    humidity = hum,
                    pressure = press
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                try
                {
                    await DeviceClient.SendEventAsync(message);
                    Debug.WriteLine("Ended sending ");
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("Error Message: {0}", e.InnerException.Message);
                }
                Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(15000).Wait();
            }
        }

    }
}
