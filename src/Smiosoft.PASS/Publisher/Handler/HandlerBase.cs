using System;
using Smiosoft.PASS.Provider;

namespace Smiosoft.PASS.Publisher.Handler
{
    internal abstract class HandlerBase
    {
        protected static THandler GetHandler<THandler>(ServiceFactory factory)
        {
            try
            {
                return factory.GetInstance<THandler>()
                    ?? throw new InvalidOperationException($"Handler was not found for handler of type {typeof(THandler)}. Register your handlers with the container.");
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Error constructing handler for handler of type {typeof(THandler)}. Register your handlers with the container.", exception);
            }
        }
    }
}
