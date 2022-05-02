using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers
{
	public class MessageOneQueueSubscriber : RabbitMqQueueSubscriber<DummyTestMessageOne>
	{
		private readonly IConnectionFactory? _factory;

		public MessageOneQueueSubscriber(string hostName, string queueName, IConnectionFactory factory) : base(hostName, queueName)
		{
			_factory = factory;
		}

		public MessageOneQueueSubscriber(string hostName, string queueName) : base(hostName, queueName)
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
