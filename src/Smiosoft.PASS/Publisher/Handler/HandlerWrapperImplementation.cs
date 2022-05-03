using System.Threading;
using System.Threading.Tasks;

namespace Smiosoft.PASS.Publisher.Handler
{
	internal class PayloadHandlerWrapperImplementation<TPayload> : HandlerWrapper
		where TPayload : IPayload
	{
		public override Task HandleAsync(IPayload payload, CancellationToken cancellationToken, ServiceFactory serviceFactory)
		{
			return GetHandler<IPublishingHandler<TPayload>>(serviceFactory).HandleAsync((TPayload)payload, cancellationToken);
		}
	}
}
