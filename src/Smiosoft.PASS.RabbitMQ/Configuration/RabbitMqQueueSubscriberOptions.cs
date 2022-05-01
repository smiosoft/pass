using Newtonsoft.Json;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqQueueSubscriberOptions : RabbitMqSubscriberOptions
	{
		public string QueueName { get; }

		[JsonConstructor]
		public RabbitMqQueueSubscriberOptions(string hostName, string queueName)
			: base(hostName)
		{
			QueueName = queueName;
		}
	}
}
