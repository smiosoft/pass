using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.RabbitMQ.Configuration;

[assembly: InternalsVisibleTo("Smiosoft.PASS.RabbitMQ.UnitTests")]
namespace Smiosoft.PASS.RabbitMQ
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPassRabbitMq(this IServiceCollection services, Action<RabbitMqOptions> options)
		{
			options(new RabbitMqOptions(services));

			services
				.AddPassPublishingService()
				.AddPassHostedSubscribersService();

			return services;
		}
	}
}
