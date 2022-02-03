using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Queue;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers
{
	public class MessageOneQueuePublisher : QueuePublisher<DummyTestMessageOne>
	{
		public MessageOneQueuePublisher(IConnectionFactory factory, string queueName) : base(factory, queueName)
		{ }

		public MessageOneQueuePublisher(string hostName, string queueName) : base(hostName, queueName)
		{ }
	}
}
