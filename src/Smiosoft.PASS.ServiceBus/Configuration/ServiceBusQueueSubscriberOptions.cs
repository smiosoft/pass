using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusQueueSubscriberOptions : ServiceBusSubscriberOptions
	{
		public string QueueName { get; }

		[JsonConstructor]
		public ServiceBusQueueSubscriberOptions(string connectionString, string queueName)
			: base(connectionString)
		{
			QueueName = queueName;
		}
	}
}