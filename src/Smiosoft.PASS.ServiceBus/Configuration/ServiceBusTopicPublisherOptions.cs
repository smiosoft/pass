using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusTopicPublisherOptions : ServiceBusPublisherOptions
	{
		public string TopicName { get; }

		[JsonConstructor]
		public ServiceBusTopicPublisherOptions(string connectionString, string topicName)
			: base(connectionString)
		{
			TopicName = topicName;
		}
	}
}
