using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MQTTnet;
using System.Collections.Generic;

namespace VolumeMixer
{
    public partial class App : Application
    {
        public static MqttConnection MqttConnection
        {
            get; set;
        }
        string ServerName = "192.168.2.62";
        int Port = 1235;
        string ClientID = "VolumeClient";
        List<string> TopicList = new List<string>()
        {
           "volume","app"
        };

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            App.MqttConnection = new MqttConnection(ServerName, Port, ClientID, "sypd", "simple", TopicList);

            await App.MqttConnection.CreateMQTTConnection();

            MqttConnection.OnConnected += MqttConnection_OnConnected;
            MqttConnection.OnDisconnected += MqttConnection_OnDisconnected;
            MqttConnection.OnErrorAtSending += MqttConnection_OnErrorAtSending;
            MqttConnection.OnMessageReceived += MqttConnection_OnMessageReceived;
        }

        private void MqttConnection_OnMessageReceived(object sender, object e)
        {
            Console.WriteLine("MQTT received a message:" + ((MqttApplicationMessageReceivedEventArgs)e).ApplicationMessage.ToString()); //ApplicationMessage.Topic, ApplicationMessage.Payload, ...
        }

        private void MqttConnection_OnErrorAtSending(object sender, object e)
        {
            Console.WriteLine("MQTT trow exception while sending: " + ((Exception)e).StackTrace);
        }

        private void MqttConnection_OnDisconnected(object sender, object e)
        {
            Console.WriteLine("MQTT Disconnected.");
        }

        private void MqttConnection_OnConnected(object sender, object e)
        {
            Console.WriteLine("MQTT Connected.");
        }

    }
}
