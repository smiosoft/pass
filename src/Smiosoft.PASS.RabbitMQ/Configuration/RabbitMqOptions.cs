using System;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.RabbitMQ.Queue;
using Smiosoft.PASS.RabbitMQ.Topic;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqOptions
	{
		private readonly IServiceCollection _services;

		public RabbitMqOptions(IServiceCollection services)
		{
			_services = services ?? throw new ArgumentNullException(nameof(services));
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
