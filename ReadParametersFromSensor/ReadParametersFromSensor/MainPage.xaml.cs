using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReadParametersFromSensor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer;
        BuildAzure.IoT.Adafruit.BME280.BME280Sensor bME280;

        const float seaLevelPressure = 1022.00f;

        public MainPage()
        {
           this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            bME280 = new BuildAzure.IoT.Adafruit.BME280.BME280Sensor();
            await bME280.Initialize();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += TimerTick;

            timer.Start();
        }
        private void TimerTick(object sender, object e)
        {
            var temperature = bME280.ReadTemperature();
            var humidity = bME280.ReadHumidity();
            var pressure = bME280.ReadPressure();
            var altitude = bME280.ReadAltitude(seaLevelPressure);

            Debug.WriteLine("Temperature: {0} deg C", temperature);
            Debug.WriteLine("Humidity: {0} %", humidity);
            Debug.WriteLine("Pressure: {0} Pa", pressure);
            Debug.WriteLine("Altitude: {0} m", altitude);

        }
    }
}
