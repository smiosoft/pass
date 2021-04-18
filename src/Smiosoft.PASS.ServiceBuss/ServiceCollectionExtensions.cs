using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.ServiceBuss.Configuration;
using System;

namespace Smiosoft.PASS.ServiceBuss
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
