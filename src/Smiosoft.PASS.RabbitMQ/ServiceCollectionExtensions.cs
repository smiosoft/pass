using System;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.RabbitMQ.Configuration;

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
