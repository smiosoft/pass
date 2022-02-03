using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Topic;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers
{
	public class MessageOneTopicSubscriber : TopicSubscriber<DummyTestMessageOne>
	{
		public MessageOneTopicSubscriber(IConnectionFactory factory, string exchangeName, string routingKey) : base(factory, exchangeName, routingKey)
		{ }

		public MessageOneTopicSubscriber(string hostName, string exchangeName, string routingKey) : base(hostName, exchangeName, routingKey)
		{ }

		public override Task OnMessageRecievedAsync(DummyTestMessageOne message, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
