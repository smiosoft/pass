using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.Subscriber;

[assembly: InternalsVisibleTo("Smiosoft.PASS.UnitTests")]
namespace Smiosoft.PASS
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPassPublisher<TPublisher>(this IServiceCollection services)
			where TPublisher : IBasePublisher
		{
			services.AddSingleton(typeof(IBasePublisher), typeof(TPublisher));

			return services;
		}

		public static IServiceCollection AddPassSubscriber<TSubscriber>(this IServiceCollection services)
			where TSubscriber : IBaseSubscriber
		{
			services.AddSingleton(typeof(IBaseSubscriber), typeof(TSubscriber));

			return services;
		}

		public static IServiceCollection AddPassPublishingService(this IServiceCollection services)
		{
			services.AddSingleton<IPublishingService, PublishingService>();

			return services;
		}

		public static IServiceCollection AddPassHostedSubscribersService(this IServiceCollection services)
		{
			services.AddHostedService<HostedSubscribersService>();

			return services;
		}
	}
}
