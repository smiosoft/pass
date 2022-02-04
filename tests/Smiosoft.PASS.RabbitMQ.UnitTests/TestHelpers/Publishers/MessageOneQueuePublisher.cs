using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers
{
	public class MessageOneQueuePublisher : RabbitMqQueuePublisher<DummyTestMessageOne>
	{
		public MessageOneQueuePublisher(IConnectionFactory factory, string queueName) : base(factory, queueName)
		{ }

		public MessageOneQueuePublisher(string hostName, string queueName) : base(hostName, queueName)
		{ }
	}
}
