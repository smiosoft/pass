using Newtonsoft.Json;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqQueuePublisherOptions : RabbitMqPublisherOptions
	{
		public string QueueName { get; }

		[JsonConstructor]
		public RabbitMqQueuePublisherOptions(string hostName, string queueName)
			: base(hostName)
		{
			QueueName = queueName;
		}
	}
}
