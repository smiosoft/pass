using System.Diagnostics.CodeAnalysis;
using Smiosoft.PASS.Provider;

namespace Smiosoft.PASS.Publisher.Handler
{
    internal abstract class HandlerBase
    {
        protected static bool TryGetHandler<THandler>(ServiceFactory services, [NotNullWhen(returnValue: true)] out THandler? handler)
        {
            handler = services.GetInstance<THandler>();
            return handler != null;
        }
    }
}
