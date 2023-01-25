using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
    public abstract class PublisherBase<TPayload> : IPublishingHandler<TPayload>, IRabbitMq
        where TPayload : IPayload
    {
        private readonly PublisherOptions _options;
        private IConnectionFactory? _factory;

        protected IConnectionFactory Factory { get => _factory ??= CreateDefaultConnectionFactory(); }

        protected PublisherBase(PublisherOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected PublisherBase(PublisherOptions options, IConnectionFactory factory)
            : this(options)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
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

        protected virtual IConnectionFactory CreateDefaultConnectionFactory()
        {
            return new ConnectionFactory() { HostName = _options.HostName };
        }
    }
}
