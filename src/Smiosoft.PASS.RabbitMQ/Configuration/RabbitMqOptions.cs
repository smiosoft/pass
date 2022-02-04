using System;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.RabbitMQ.Subscriber;

namespace Smiosoft.PASS.RabbitMQ.Configuration
{
	public class RabbitMqOptions
	{
		private readonly IServiceCollection _services;

		public RabbitMqOptions(IServiceCollection services)
		{
			_services = services ?? throw new ArgumentNullException(nameof(services));
		}

		public void AddPublisher<TPublisher>()
			where TPublisher : class, IRabbitMqPublisher
		{
			_services.AddPassPublisher<TPublisher>();
		}

		public void AddSubscriber<TSubscriber>()
			where TSubscriber : class, IRabbitMqSubscriber
		{
			_services.AddPassSubscriber<TSubscriber>();
		}
	}
}
