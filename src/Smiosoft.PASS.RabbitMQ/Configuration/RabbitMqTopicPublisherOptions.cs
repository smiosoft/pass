﻿using Newtonsoft.Json;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqTopicPublisherOptions : RabbitMqPublisherOptions
	{
		public string ExchangeName { get; }
		public string RoutingKey { get; }

		[JsonConstructor]
		public RabbitMqTopicPublisherOptions(string hostName, string exchangeName, string routingKey)
			: base(hostName)
		{
			ExchangeName = exchangeName;
			RoutingKey = routingKey;
		}
	}
}
