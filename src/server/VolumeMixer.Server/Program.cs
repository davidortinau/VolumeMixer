using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Mqtt;
using System.Threading.Tasks;

namespace VolumeMixer.Server
{
	class Program
	{
		const int loopDelayTime = 250;
		const string topic = "test/chat/message";
		const string exitMessage = "exit";

		static async Task Main(string[] args)
		{
			bool isFinishing = false;
			var deviceIpAddresses = GetLocalIPAddresses();
			var config = new MqttConfiguration { Port = 1235 };
			var server = MqttServer.Create(config);

			server.Start();

			var client = await server.CreateClientAsync();
			var clientId = client.Id;
			var received = "";

			await client.SubscribeAsync(topic, MqttQualityOfService.AtLeastOnce);
			client.MessageStream.Subscribe(message => {
				if (isFinishing)
					return;
				var data = Encoding.UTF8.GetString(message.Payload).Split(new string[] { ":" }, StringSplitOptions.None);
				Console.WriteLine($"Message Received from {data[0]}: {data[1]}");

				received = data[1];

				//PublishAsync(client, clientId, exitMessage); // don't exit, stay open for business

				if (received == exitMessage)
					isFinishing = true;
			});

			Console.WriteLine($"Server [{String.Join("/", deviceIpAddresses)}]:{config.Port} with topic '{topic}' is up.");
	
			Console.WriteLine($"Awaiting messages...");

			while (received != exitMessage)
			{
				await Task.Delay(loopDelayTime);
			}
			Console.WriteLine("Shutting down... Received exit command.");
		}

		static Task PublishAsync(IMqttConnectedClient client, string clientId, string message)
		{
			Console.WriteLine($"Sending message to {clientId}: {message}");
			return client.PublishAsync(new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes($"{clientId}:{message}")), MqttQualityOfService.AtLeastOnce);
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
