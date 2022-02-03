using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Queue;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers
{
	public class MessageOneQueueSubscriber : QueueSubscriber<DummyTestMessageOne>
	{
		public MessageOneQueueSubscriber(IConnectionFactory factory, string queueName) : base(factory, queueName)
		{ }

		public MessageOneQueueSubscriber(string hostName, string queueName) : base(hostName, queueName)
		{ }

		public override Task OnMessageRecievedAsync(DummyTestMessageOne message, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
