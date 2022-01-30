using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.ServiceBus.Configuration;

[assembly: InternalsVisibleTo("Smiosoft.PASS.ServiceBus.UnitTests")]
namespace Smiosoft.PASS.ServiceBus
{
	public static class ServiceCollectionExtensions
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
