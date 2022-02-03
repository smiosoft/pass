using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.Subscriber;

[assembly: InternalsVisibleTo("Smiosoft.PASS.UnitTests")]
namespace Smiosoft.PASS
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPassPublisher<TPublisherImplementation>(this IServiceCollection services)
			where TPublisherImplementation : IBasePublisher
		{
			services.AddSingleton(typeof(IBasePublisher), typeof(TPublisherImplementation));

			return services;
		}

		public static IServiceCollection AddPassSubscriber<TSubscriberImplementation>(this IServiceCollection services)
			where TSubscriberImplementation : IBaseSubscriber
		{
			services.AddSingleton(typeof(IBaseSubscriber), typeof(TSubscriberImplementation));

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
