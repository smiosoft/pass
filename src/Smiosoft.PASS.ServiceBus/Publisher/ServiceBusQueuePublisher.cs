using System;
using System.Threading.Tasks;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class ServiceBusQueuePublisher<TMessage> : ServiceBusPublisherBase<TMessage>
		where TMessage : class
	{
		protected ServiceBusQueuePublisherOptions Options { get; }

		protected ServiceBusQueuePublisher(ServiceBusQueuePublisherOptions queuePublisherOptions)
			: base(queuePublisherOptions)
		{
			Options = queuePublisherOptions ?? throw new ArgumentNullException(nameof(queuePublisherOptions));
		}

		protected ServiceBusQueuePublisher(string connectionString, string queueName)
			: this(new ServiceBusQueuePublisherOptions(connectionString, queueName))
		{ }

		public override Task PublishAsync(TMessage message)
		{
			return SendMessageAsync(Options.QueueName, message);
		}
	}
}
