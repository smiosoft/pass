using System;
using System.Threading.Tasks;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class ServiceBusTopicPublisher<TMessage> : ServiceBusPublisherBase<TMessage>
		where TMessage : class
	{
		protected ServiceBusTopicPublisherOptions Options { get; }

		protected ServiceBusTopicPublisher(ServiceBusTopicPublisherOptions topicPublisherOptions)
			: base(topicPublisherOptions)
		{
			Options = topicPublisherOptions ?? throw new ArgumentNullException(nameof(topicPublisherOptions));
		}

		public override Task PublishAsync(TMessage message)
		{
			return SendMessageAsync(Options.TopicName, message);
		}
	}
}
