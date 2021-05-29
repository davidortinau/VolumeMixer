using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VolumeMixer.Pages
{
    public partial class MainPage
    {
        const int loopDelayTime = 250;
        const string topic = "test/chat/message";
        const string exitMessage = "exit";

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
        }

        static void PublishAsync(IMqttClient client, string clientId, string message)
        {
            Console.WriteLine($"Sending message: {message}");
            client.PublishAsync(new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes($"{clientId}:{message}")), MqttQualityOfService.AtLeastOnce).Wait();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            try
            {

            
            bool isFinishing = false;
            var config = new MqttConfiguration {
                BufferSize = 128 * 1024,
                Port = 1235,
                KeepAliveSecs = 10,
                WaitTimeoutSecs = 2,
                MaximumQualityOfService = MqttQualityOfService.AtMostOnce,
                AllowWildcardsInTopicFilters = true
            };
            var client = MqttClient.CreateAsync("192.168.2.62", config).Result;
            var clientId = "XamarinClient";
            var received = "";

            client.ConnectAsync(new MqttClientCredentials(clientId)).Wait();
            client.SubscribeAsync(topic, MqttQualityOfService.AtLeastOnce).Wait();
            client.MessageStream.Subscribe(message => {
                if (isFinishing)
                    return;

                var data = Encoding.UTF8.GetString(message.Payload).Split(new string[] { ":" }, StringSplitOptions.None);
                Console.WriteLine($"Message Received from {data[0]}:{data[1]}");
                PublishAsync(client, clientId, exitMessage);

                //Send exit to server
                received = data[1];

                if (received == exitMessage)
                    isFinishing = true;
            });

            Console.WriteLine($"Client connected successfully to {client.Id}:{config.Port}");
            Console.WriteLine($"Awaiting messages...");

            PublishAsync(client, clientId, "Connected.");

            while (received != exitMessage)
            {
                Thread.Sleep(loopDelayTime);
            }
            Console.WriteLine("Shutting down... Received exit command.");
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
