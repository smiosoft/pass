using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.ServiceBus.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers
{
	public class MessageOneQueueSubscriber : ServiceBusQueueSubscriber<DummyTestMessageOne>
	{
		private readonly ServiceBusProcessor? _processor;

		public MessageOneQueueSubscriber(ServiceBusProcessor processor, string connectionString, string queueName)
			: base(connectionString, queueName)
		{
			_processor = processor;
		}

		public MessageOneQueueSubscriber(string connectionString, string queueName)
			: base(connectionString, queueName)
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
