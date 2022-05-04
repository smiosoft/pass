using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Subscriber
{
	/// <summary>
	/// Defines a handler for a payload subscription
	/// </summary>
	/// <typeparam name="TPayload">The type of payload being handled</typeparam>
	public interface ISubscriptionHandler<in TPayload> : IDomain
		where TPayload : IPayload
	{
		/// <summary>
		/// Handles an incoming payload
		/// </summary>
		/// <param name="payload">The payload</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>An awaitable task</returns>
		Task HandleAsync(TPayload payload, CancellationToken cancellationToken);
	}
}
