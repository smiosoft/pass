using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.Subscriber;

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

		public static IServiceCollection AddPassPublishersService(this IServiceCollection services)
		{
			services.AddSingleton<IPublishersService, PublishersService>();

			return services;
		}

		public static IServiceCollection AddPassSubscribersService(this IServiceCollection services)
		{
			services.AddHostedService<HostedSubscribersService>();

			return services;
		}
	}
}
