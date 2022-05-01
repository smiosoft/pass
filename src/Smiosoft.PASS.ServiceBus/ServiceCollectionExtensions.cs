using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.ServiceBus.Configuration;

[assembly: InternalsVisibleTo("Smiosoft.PASS.ServiceBus.UnitTests")]
namespace Smiosoft.PASS.ServiceBus
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPassServiceBus(this IServiceCollection services, Action<ConfigureServiceBusOptions> setup)
		{
			setup(new ConfigureServiceBusOptions(services));

			services
				.AddPassPublishingService()
				.AddPassHostedSubscribersService();

			return services;
		}
	}
}
