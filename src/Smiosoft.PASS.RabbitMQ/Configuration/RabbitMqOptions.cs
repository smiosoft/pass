using Newtonsoft.Json;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqOptions
	{
		public string HostName { get; }

		[JsonConstructor]
		public RabbitMqOptions(string hostName)
		{
			HostName = hostName;
		}
	}
}
