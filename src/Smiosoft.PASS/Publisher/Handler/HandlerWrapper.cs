using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Publisher.Handler
{
	internal abstract class HandlerWrapper : HandlerBase
	{
		public abstract Task HandleAsync(IPayload payload, CancellationToken cancellationToken, ServiceFactory serviceFactory);
	}
}
