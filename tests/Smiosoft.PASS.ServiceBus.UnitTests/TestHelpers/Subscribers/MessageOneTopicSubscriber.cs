using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.ServiceBus.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers
{
	public class MessageOneTopicSubscriber : ServiceBusTopicSubscriber<DummyTestMessageOne>
	{
		private readonly ServiceBusProcessor? _processor;

		public MessageOneTopicSubscriber(ServiceBusProcessor processor, string connectionString, string topicName, string subscriptionName)
			: base(connectionString, topicName, subscriptionName)
		{
			_processor = processor;
		}

		public MessageOneTopicSubscriber(string connectionString, string topicName, string subscriptionName)
			: base(connectionString, topicName, subscriptionName)
		{ }

		public override Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public override Task OnMessageRecievedAsync(DummyTestMessageOne message, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		protected override ServiceBusProcessor CreateProcessor()
		{
			return _processor!;
		}
	}
}
