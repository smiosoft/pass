using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Topic;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers
{
	public class MessageOneTopicPublisher : TopicPublisher<DummyTestMessageOne>
	{
		public MessageOneTopicPublisher(IConnectionFactory factory, string exchangeName, string routingKey) : base(factory, exchangeName, routingKey)
		{ }

		public MessageOneTopicPublisher(string hostName, string exchangeName, string routingKey) : base(hostName, exchangeName, routingKey)
		{ }
	}
}
