﻿using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Mqtt;

namespace VolumeMixer.Server
{
	class Program
	{
		const int loopDelayTime = 250;
		const string topic = "test/chat/message";
		const string exitMessage = "exit";

		static void Main(string[] args)
		{
			bool isFinishing = false;
			var deviceIpAddresses = GetLocalIPAddresses();
			var config = new MqttConfiguration { Port = 1235 };
			var server = MqttServer.Create(config);

			server.Start();

			var client = server.CreateClientAsync().Result;
			var clientId = client.Id;
			var received = "";

			client.SubscribeAsync(topic, MqttQualityOfService.AtLeastOnce).Wait();
			client.MessageStream.Subscribe(message => {
				if (isFinishing)
					return;
				var data = Encoding.UTF8.GetString(message.Payload).Split(new string[] { ":" }, StringSplitOptions.None);
				Console.WriteLine($"Message Received from {data[0]}: {data[1]}");

				received = data[1];

				PublishAsync(client, clientId, exitMessage);

				if (received == exitMessage)
					isFinishing = true;
			});

			Console.WriteLine($"Server [{String.Join("/", deviceIpAddresses)}]:{config.Port} with topic '{topic}' is up.");
	
			Console.WriteLine($"Awaiting messages...");

			while (received != exitMessage)
			{
				Thread.Sleep(loopDelayTime);
			}
			Console.WriteLine("Shutting down... Received exit command.");
		}

		static void PublishAsync(IMqttConnectedClient client, string clientId, string message)
		{
			Console.WriteLine($"Sending message to {clientId}: {message}");
			client.PublishAsync(new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes($"{clientId}:{message}")), MqttQualityOfService.AtLeastOnce).Wait();
		}

		public static List<string> GetLocalIPAddresses()
		{
			var addrs = new List<string>();
			
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
					addrs.Add(ip.ToString());
			}
			
			if (!addrs.Any())
				throw new Exception("Local IP Address Not Found!");

			return addrs;
		}
	}
}
