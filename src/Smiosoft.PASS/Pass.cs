using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Provider;
using Smiosoft.PASS.Publisher.Handler;

[assembly: InternalsVisibleTo("Smiosoft.PASS.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Smiosoft.PASS
{
    internal class Pass : IPass
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, HandlerBase> _handlers = new();

        public Pass(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        }

        public Task PublishAsync(IPayload payload, CancellationToken cancellationToken = default)
        {
            var payloadType = payload.GetType();
            var handler = (HandlerWrapper)_handlers.GetOrAdd(payloadType, static implementation =>
            {
                return (HandlerBase)Activator.CreateInstance(typeof(HandlerWrapperImplementation<>).MakeGenericType(implementation)
                    ?? throw new InvalidOperationException($"Could not create wrapper for {implementation} type"));
            });

            return handler.HandleAsync(payload, cancellationToken, _serviceFactory);
        }

        public async Task<bool> TryPublishAsync(IPayload payload, CancellationToken cancellationToken = default)
        {
            try
            {
                await PublishAsync(payload, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
