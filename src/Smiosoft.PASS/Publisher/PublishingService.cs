using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Smiosoft.PASS.Publisher
{
	internal class PublishingService : IPublishingService
	{
		private readonly IServiceProvider _provider;
		private readonly ConcurrentDictionary<Type, object> _publishers;

		public PublishingService(IServiceProvider provider)
		{
			_provider = provider ?? throw new ArgumentNullException(nameof(provider));
			_publishers = new ConcurrentDictionary<Type, object>();
		}

		public Task PublishAsync<TMessage>(TMessage message) where TMessage : class
		{
			var messageType = typeof(TMessage);
			var publisher = (IMessagePublisher<TMessage>)_publishers.GetOrAdd(messageType, (type) =>
			{
				var publishers = _provider.GetServices<IBasePublisher>();
				var implementation = publishers.FirstOrDefault(service => typeof(IMessagePublisher<TMessage>).IsAssignableFrom(service.GetType()));
				return implementation ?? throw new PublisherNotRegisteredException($"Could not find publisher implementation for message type: [{messageType.Name}]");
			});

			return publisher.PublishAsync(message);
		}

		public async Task<bool> TryPublishAsync<TMessage>(TMessage message) where TMessage : class
		{
			try
			{
				await PublishAsync(message);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
