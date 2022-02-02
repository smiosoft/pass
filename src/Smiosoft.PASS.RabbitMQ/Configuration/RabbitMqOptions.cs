using System;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.RabbitMQ.Publisher;

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
	}
}
