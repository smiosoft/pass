using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.ServiceBus.Topic;
using Smiosoft.PASS.UnitTests.Helpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Publishers
{
	public class MessageOneTopicPublisher : TopicPublisher<DummyTestMessageOne>
	{
		public MessageOneTopicPublisher(ITopicClient client) : base(client)
		{ }

		public MessageOneTopicPublisher(string connectionString, string topicPath) : base(connectionString, topicPath)
		{ }
	}
}
