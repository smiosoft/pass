using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.ServiceBus.Queue;
using Smiosoft.PASS.UnitTests.Helpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Publishers
{
	public class MessageOneQueuePublisher : QueuePublisher<DummyTestMessageOne>
	{
		public MessageOneQueuePublisher(IQueueClient client) : base(client)
		{ }

		public MessageOneQueuePublisher(string connectionString, string queueName) : base(connectionString, queueName)
		{ }
	}
}
