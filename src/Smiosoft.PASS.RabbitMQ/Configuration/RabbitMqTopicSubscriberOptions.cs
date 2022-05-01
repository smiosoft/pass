using Newtonsoft.Json;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqTopicSubscriberOptions : RabbitMqSubscriberOptions
	{
		public string ExchangeName { get; }
		public string RoutingKey { get; }

		[JsonConstructor]
		public RabbitMqTopicSubscriberOptions(string hostName, string exchangeName, string routingKey)
			: base(hostName)
		{
			ExchangeName = exchangeName;
			RoutingKey = routingKey;
		}
	}
}
