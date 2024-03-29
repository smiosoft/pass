using System.Threading;
using System.Threading.Tasks;

namespace Smiosoft.PASS.Subscriber
{
    /// <summary>
    /// Defines a listener service for a subscrption
    /// </summary>
    public interface IListener : IDomain
    {
        /// <summary>
        /// Register the listening service
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An awaitable task</returns>
        Task RegisterAsync(CancellationToken cancellationToken = default);
    }
}
