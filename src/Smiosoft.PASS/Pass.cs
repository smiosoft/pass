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
        private static readonly ConcurrentDictionary<Type, HandlerBase> s_handlers = new();
        private readonly ServiceFactory _serviceFactory;

        public Pass(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        }

        public Task PublishAsync(IPayload payload, CancellationToken cancellationToken = default)
        {
            var handler = GetHandlerImplementationForPayload(payload);
            return handler.HandleAsync(payload, cancellationToken, _serviceFactory);
        }

        public Task<bool> TryPublishAsync(IPayload payload, CancellationToken cancellationToken = default)
        {
            var handler = GetHandlerImplementationForPayload(payload);
            return handler.TryHandleAsync(payload, cancellationToken, _serviceFactory);
        }

        private HandlerWrapper GetHandlerImplementationForPayload(IPayload payload)
        {
            return (HandlerWrapper)s_handlers.GetOrAdd(payload.GetType(), static implementation =>
            {
                return (HandlerBase)Activator.CreateInstance(typeof(HandlerWrapperImplementation<>).MakeGenericType(implementation)
                    ?? throw new InvalidOperationException($"Could not create wrapper for {implementation} type"));
            });
        }
    }
}
