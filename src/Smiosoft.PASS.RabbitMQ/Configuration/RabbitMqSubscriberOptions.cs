using Newtonsoft.Json;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqSubscriberOptions : RabbitMqOptions
	{
		[JsonConstructor]
		public RabbitMqSubscriberOptions(string hostName)
			: base(hostName)
		{ }
	}
}
