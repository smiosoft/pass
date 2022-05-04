using System;

namespace Smiosoft.PASS.Publisher.Handler
{
	internal abstract class HandlerBase
	{
		protected static THandler GetHandler<THandler>(ServiceFactory factory)
		{
			try
			{
				return factory.GetInstance<THandler>()
					?? throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container.");
			}
			catch (Exception exception)
			{
				throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container.", exception);
			}
		}
	}
}
