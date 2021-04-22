using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.ServiceBus.Queue;
using Smiosoft.PASS.ServiceBus.Topic;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusOptions
	{
		private readonly IServiceCollection _services;

		public ServiceBusOptions(IServiceCollection services)
		{
			_services = services;
		}

		public void AddQueuePublisher<TQueuePublisherImplementation>()
			where TQueuePublisherImplementation : class, IQueuePublisher
		{
			_services.AddPassPublisher<TQueuePublisherImplementation>();
		}

		public void AddQueueSubscriber<TQueueSubscriberImplementation>()
			where TQueueSubscriberImplementation : class, IQueueSubscriber
		{
			_services.AddPassSubscriber<TQueueSubscriberImplementation>();
		}

		public void AddTopicPublisher<TTopicPublisherImplementation>()
			where TTopicPublisherImplementation : class, ITopicPublisher
		{
			_services.AddPassPublisher<TTopicPublisherImplementation>();
		}

		public void AddTopicSubscriber<TTopicSubscriberImplementation>()
			where TTopicSubscriberImplementation : class, ITopicSubscriber
		{
			_services.AddPassSubscriber<TTopicSubscriberImplementation>();
		}
	}
}
