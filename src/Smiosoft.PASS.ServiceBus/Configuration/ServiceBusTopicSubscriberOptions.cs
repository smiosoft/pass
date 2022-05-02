using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusTopicSubscriberOptions : ServiceBusSubscriberOptions
	{
		public string TopicName { get; }
		public string SubscriptionName { get; }

		[JsonConstructor]
		public ServiceBusTopicSubscriberOptions(string connectionString, string topicName, string subscriptionName)
			: base(connectionString)
		{
			TopicName = topicName;
			SubscriptionName = subscriptionName;
		}
	}
}
