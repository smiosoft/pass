namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusTopicSubscriberOptions : ServiceBusSubscriberOptions
	{
		public string TopicName { get; set; } = string.Empty;
		public string SubscriptionName { get; set; } = string.Empty;
	}
}
