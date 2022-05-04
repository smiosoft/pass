using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers
{
	public static class Subscribers
	{
		public class QueueSubscriberOne : QueueSubscriber<Payloads.DummyPayloadOne>
		{
			private readonly IConnectionFactory? _factory;

			public QueueSubscriberOne(string hostName, string queueName, IConnectionFactory? factory)
				: base(hostName, queueName)
			{
				_factory = factory;
			}

			public override Task OnExceptionAsync(Exception exception)
			{
				return Task.CompletedTask;
			}

			public override Task OnReceivedAsync(Payloads.DummyPayloadOne payload, CancellationToken cancellationToken = default)
			{
				return Task.CompletedTask;
			}

			protected override IConnectionFactory CreateConnectionFactory()
			{
				return _factory ?? base.CreateConnectionFactory();
			}
		}

		public class TopicSubscriberOne : TopicSubscriber<Payloads.DummyPayloadOne>
		{
			private readonly IConnectionFactory? _factory;

			public TopicSubscriberOne(string hostName, string exchangeName, string routingKey, IConnectionFactory? factory)
				: base(hostName, exchangeName, routingKey)
			{
				_factory = factory;
			}

			public override Task OnExceptionAsync(Exception exception)
			{
				return Task.CompletedTask;
			}

			public override Task OnReceivedAsync(Payloads.DummyPayloadOne payload, CancellationToken cancellationToken = default)
			{
				return Task.CompletedTask;
			}

			protected override IConnectionFactory CreateConnectionFactory()
			{
				return _factory ?? base.CreateConnectionFactory();
			}
		}
	}
}
