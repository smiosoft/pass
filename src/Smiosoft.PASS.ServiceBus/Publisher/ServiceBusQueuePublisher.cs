using System;
using System.Threading.Tasks;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class ServiceBusQueuePublisher<TMessage> : ServiceBusPublisherBase<TMessage>
		where TMessage : class
	{
		protected ServiceBusQueuePublisherOptions QueuePublisherOptions { get; }

		protected ServiceBusQueuePublisher(ServiceBusQueuePublisherOptions queuePublisherOptions)
			: base(queuePublisherOptions)
		{
			QueuePublisherOptions = queuePublisherOptions ?? throw new ArgumentNullException(nameof(queuePublisherOptions));
		}

		public override Task PublishAsync(TMessage message)
		{
			return SendMessageAsync(QueuePublisherOptions.QueueName, message);
		}
	}
}
