using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.ServiceBuss.Topic;

namespace Smiosoft.PASS.ServiceBuss.Configuration
{
	public class ServiceBusOptions
	{
		private readonly IServiceCollection _services;

		public ServiceBusOptions(IServiceCollection services)
		{
			_services = services;
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
