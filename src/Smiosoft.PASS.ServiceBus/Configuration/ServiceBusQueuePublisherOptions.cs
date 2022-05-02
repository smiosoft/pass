using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusQueuePublisherOptions : ServiceBusPublisherOptions
	{
		public string QueueName { get; }

		[JsonConstructor]
		public ServiceBusQueuePublisherOptions(string connectionString, string queueName)
			: base(connectionString)
		{
			QueueName = queueName;
		}
	}
}
