using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.ServiceBus.Topic;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers
{
	public class MessageOneTopicSubscriber : TopicSubscriber<DummyTestMessageOne>
	{
		public MessageOneTopicSubscriber(ISubscriptionClient client) : base(client)
		{ }

		public MessageOneTopicSubscriber(string connectionString, string topicPath, string subscriptionName) : base(connectionString, topicPath, subscriptionName)
		{ }

		public override Task OnMessageRecievedAsync(DummyTestMessageOne message, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
