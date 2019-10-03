using System;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.Azure.Devices.Client;

namespace ReadParametersFromSensor
{
    public class SendDataToIotHub
    {
        private static DeviceClient deviceClient;
        //private readonly static string connectionString = "HostName=IoTHubMember.azure-devices.net;DeviceId=MyRpi;SharedAccessKey=F4CptKs4Pn9bc1oi43GXsqFtgsKAFnEd2q5XA53rKRk=";
        //private readonly static string iotHubUri = "IoTHubMember.azure-devices.net";
        //private readonly static string deviceId = "MyRpi";
        //private readonly static string deviceKey = "F4CptKs4Pn9bc1oi43GXsqFtgsKAFnEd2q5XA53rKRk=";

        public static async void SendDataToAzure()
        {
            var text = "HELLO";
            //deviceClient = DeviceClient.Create(iotHubUri, AuthenticationMethodFactory.CreateAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey), TransportType.Http1);
            //deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Amqp);
            while (true)
            {
                var textcurrent = text + "+";
                var telemetryDataPoint = new { textcurrent };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                //Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await deviceClient.SendEventAsync(message);
                Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000);
            }
        }
    }
}
