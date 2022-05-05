using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class PublisherBase<TPayload> : IPublishingHandler<TPayload>, IServiceBus
		where TPayload : IPayload
	{
		private readonly PublisherOptions _options;

		public PublisherBase(PublisherOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		public async Task HandleAsync(TPayload payload, CancellationToken cancellationToken)
		{
			try
			{
				await OnPublishAsync(payload, cancellationToken);
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}

		public abstract Task OnExceptionAsync(Exception exception);
		public abstract Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken);

		protected virtual ServiceBusClient CreateClient()
		{
			return new ServiceBusClient(_options.ConnectionString);
		}
	}
}
