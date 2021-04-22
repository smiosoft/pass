using System;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus
{
	public static class StartupInjection
	{
		public static IServiceCollection AddPassServiceBus(this IServiceCollection services, Action<ServiceBusOptions> options)
		{
			options(new ServiceBusOptions(services));

			services
				.AddPassPublishersService()
				.AddPassSubscribersService();

			return services;
		}
	}
}
