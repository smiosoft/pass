using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Publisher.Handler
{
	internal class HandlerWrapperImplementation<TPayload> : HandlerWrapper
		where TPayload : IPayload
	{
		public override Task HandleAsync(IPayload payload, CancellationToken cancellationToken, ServiceFactory serviceFactory)
		{
			return GetHandler<IPublishingHandler<TPayload>>(serviceFactory).HandleAsync((TPayload)payload, cancellationToken);
		}
	}
}
