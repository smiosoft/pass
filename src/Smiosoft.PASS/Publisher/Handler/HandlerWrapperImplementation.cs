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
            return GetHandler<IPublishingHandler<TPayload>>(serviceFactory).HandleAsync((TPayload)payload, cancellationToken);
        }

        public override Task<bool> TryHandleAsync(IPayload payload, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            return GetHandler<IPublishingHandler<TPayload>>(serviceFactory).TryHandleAsync((TPayload)payload, cancellationToken);
        }
    }
}
