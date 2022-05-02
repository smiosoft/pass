using System;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.ServiceBus.Publisher;
using Smiosoft.PASS.ServiceBus.Subscriber;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ConfigureServiceBusOptions
	{
		private readonly IServiceCollection _services;

		public ConfigureServiceBusOptions(IServiceCollection services)
		{
			_services = services ?? throw new ArgumentNullException(nameof(services));
		}

		public void AddPublisher<TPublisher>()
			where TPublisher : class, IServiceBusPublisher
		{
			_services.AddPassPublisher<TPublisher>();
		}

		public void AddSubscriber<TSubscriber>()
			where TSubscriber : class, IServiceBusSubscriber
		{
			_services.AddPassSubscriber<TSubscriber>();
		}
	}
}
