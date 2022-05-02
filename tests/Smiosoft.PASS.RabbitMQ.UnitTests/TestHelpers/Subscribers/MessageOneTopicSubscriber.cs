using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers
{
	public class MessageOneTopicSubscriber : RabbitMqTopicSubscriber<DummyTestMessageOne>
	{
		private readonly IConnectionFactory? _factory;

		public MessageOneTopicSubscriber(string hostName, string exchangeName, string routingKey, IConnectionFactory factory) : base(hostName, exchangeName, routingKey)
		{
			_factory = factory;
		}

		public MessageOneTopicSubscriber(string hostName, string exchangeName, string routingKey) : base(hostName, exchangeName, routingKey)
		{ }

		public override Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public override Task OnMessageRecievedAsync(DummyTestMessageOne message, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		protected override IConnectionFactory CreateConnectionFactory()
		{
			return _factory ?? base.CreateConnectionFactory();
		}
	}
}
