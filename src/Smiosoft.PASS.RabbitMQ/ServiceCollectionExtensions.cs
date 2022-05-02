using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.RabbitMQ.Configuration;

[assembly: InternalsVisibleTo("Smiosoft.PASS.RabbitMQ.UnitTests")]
namespace Smiosoft.PASS.RabbitMQ
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPassRabbitMq(this IServiceCollection services, Action<ConfigureRabbitMqOptions> setup)
		{
			setup(new ConfigureRabbitMqOptions(services));

			services
				.AddPassPublishingService()
				.AddPassHostedSubscribersService();

			return services;
		}
	}
}
