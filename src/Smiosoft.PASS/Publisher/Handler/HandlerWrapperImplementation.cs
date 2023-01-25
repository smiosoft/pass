using System;
using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Provider;

namespace Smiosoft.PASS.Publisher.Handler
{
    internal class HandlerWrapperImplementation<TPayload> : HandlerWrapper
        where TPayload : IPayload
    {
        public override Task HandleAsync(IPayload payload, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            if (!TryGetHandler<IPublishingHandler<TPayload>>(serviceFactory, out var handler))
            {
                throw new InvalidOperationException($"Handler was not found for handler of type {typeof(TPayload)}. Remember to register your handlers with the container.");
            }
            return handler.HandleAsync((TPayload)payload, cancellationToken);
        }

        public override Task<bool> TryHandleAsync(IPayload payload, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            if (!TryGetHandler<IPublishingHandler<TPayload>>(serviceFactory, out var handler))
            {
                return Task.FromResult(false);
            }
            return handler.TryHandleAsync((TPayload)payload, cancellationToken);
        }
    }
}
