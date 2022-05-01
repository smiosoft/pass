namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusQueueSubscriberOptions : ServiceBusSubscriberOptions
	{
		public string QueueName { get; set; } = string.Empty;
	}
}
