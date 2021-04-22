using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.ServiceBus.Queue;
using Smiosoft.PASS.UnitTests.Helpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Subscribers
{
	public class MessageOneQueueSubscriber : QueueSubscriber<DummyTestMessageOne>
	{
		public MessageOneQueueSubscriber(IQueueClient client) : base(client)
		{ }

		public MessageOneQueueSubscriber(string connectionString, string queueName) : base(connectionString, queueName)
		{ }

		public override Task OnMessageRecievedAsync(DummyTestMessageOne message, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
