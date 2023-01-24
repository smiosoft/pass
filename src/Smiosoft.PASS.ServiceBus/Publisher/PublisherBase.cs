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
            await OnPublishAsync(payload, cancellationToken);
        }

        public async Task<bool> TryHandleAsync(TPayload payload, CancellationToken cancellationToken)
        {
            try
            {
                await HandleAsync(payload, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public abstract Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken);

        protected virtual ServiceBusClient CreateDefaultClient()
        {
            return new ServiceBusClient(_options.ConnectionString);
        }
    }
}
