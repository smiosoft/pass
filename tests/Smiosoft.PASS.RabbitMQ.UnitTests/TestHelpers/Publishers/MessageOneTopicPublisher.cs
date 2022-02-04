using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers
{
	public class MessageOneTopicPublisher : RabbitMqTopicPublisher<DummyTestMessageOne>
	{
		public MessageOneTopicPublisher(IConnectionFactory factory, string exchangeName, string routingKey) : base(factory, exchangeName, routingKey)
		{ }

		public MessageOneTopicPublisher(string hostName, string exchangeName, string routingKey) : base(hostName, exchangeName, routingKey)
		{ }
	}
}
