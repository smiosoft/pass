using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Publisher.Handler;

namespace Smiosoft.PASS
{
	internal class Pass : IPass
	{
		private readonly ServiceFactory _serviceFactory;
		private static readonly ConcurrentDictionary<Type, HandlerBase> _handlers = new();

		public Pass(ServiceFactory serviceFactory)
		{
			_serviceFactory = serviceFactory;
		}

		public Task PublishAsync(IPayload payload, CancellationToken cancellationToken = default)
		{
			var requestType = payload.GetType();
			var handler = (HandlerWrapper)_handlers.GetOrAdd(requestType, static implementation =>
			{
				return (HandlerBase)Activator.CreateInstance(typeof(HandlerWrapperImplementation<>).MakeGenericType(implementation)
					?? throw new InvalidOperationException($"Could not create wrapper type for {implementation}"));
			});

			return handler.HandleAsync(payload, cancellationToken, _serviceFactory);
		}
	}
}
