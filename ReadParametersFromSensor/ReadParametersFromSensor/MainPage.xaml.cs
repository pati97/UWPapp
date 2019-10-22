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
        private static string iotHubUri = "IoTHubMember.azure-devices.net";
        private static string deviceId = "MyRpi";
        private static string deviceKey = "F4CptKs4Pn9bc1oi43GXsqFtgsKAFnEd2q5XA53rKRk=";
        public MainPage()
        {
            this.InitializeComponent();
            DeviceClient = DeviceClient.CreateFromConnectionString(connectionStringStandart, TransportType.Amqp);
        }

        private async void SendDeviceToCloudMessageAsync(double temp, float humidity, float pressure, float altitude)
        {
            //if(DeviceClient == null)
            //{
            //    DeviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey), TransportType.Amqp);
            //}
            while (true)
            {
                var telemetryDataPoint = new {
                messageId = messageId++,
                deviceId = deviceID,
                dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                temperature = temp,
                humidity = humidity,
                pressure = pressure
                };
              
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                
                //Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now.ToLocalTime(), messageString);

                try
                {
                    await DeviceClient.SendEventAsync(message);
                    Debug.WriteLine("Ended sending ");  
                }
                catch(HttpRequestException e)
                {
                    Debug.WriteLine("Error Message: {0}",e.InnerException.Message);
                }
                Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(15000).Wait();
            }
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            bME280 = new BuildAzure.IoT.Adafruit.BME280.BME280Sensor();
            await bME280.Initialize();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += TimerTick;

            timer.Start();
        }

        private async void TimerTick(object sender, object e)
        {
            try
            {
                await ReadParameters();  
            }
            catch
            {
                Debug.WriteLine("Cannot read value from sensor...");
            }
        }
        private async Task ReadParameters()
        {
            var temperature = await bME280.ReadTemperature();
            var humidity = await bME280.ReadHumidity();
            var pressure = await bME280.ReadPressure() / 100;
            var altitude = await bME280.ReadAltitude(seaLevelPressure);

            Debug.WriteLine("Temperature: {0} deg C", temperature);
            Debug.WriteLine("Humidity: {0} %", humidity);
            Debug.WriteLine("Pressure: {0} hPa", pressure);
            Debug.WriteLine("Altitude: {0} m", altitude);
            SendDeviceToCloudMessageAsync(temperature, humidity, pressure, altitude);
        }
    }
}
