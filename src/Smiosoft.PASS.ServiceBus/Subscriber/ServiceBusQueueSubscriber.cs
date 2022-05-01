using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class ServiceBusQueueSubscriber<TMessage> : ServiceBusSubscriberBase<TMessage>
		where TMessage : class
	{
		protected ServiceBusQueueSubscriberOptions Options { get; }

		protected ServiceBusQueueSubscriber(ServiceBusQueueSubscriberOptions queueSubscriberOptions)
			: base(queueSubscriberOptions)
		{
			Options = queueSubscriberOptions ?? throw new ArgumentNullException(nameof(queueSubscriberOptions));
		}

		public override Task RegisterAsync()
		{
			return SetupProcessorAsync();
		}

		protected override ServiceBusProcessor CreateProcessor()
		{
			return Client.CreateProcessor(queueName: Options.QueueName);
		}
	}
}
