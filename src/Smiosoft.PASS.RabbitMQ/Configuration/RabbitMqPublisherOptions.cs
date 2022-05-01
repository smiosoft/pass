using Newtonsoft.Json;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqPublisherOptions : RabbitMqOptions
	{
		[JsonConstructor]
		public RabbitMqPublisherOptions(string hostName)
			: base(hostName)
		{ }
	}
}
