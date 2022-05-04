using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Subscriber
{
	/// <summary>
	/// Defines a handler for subscribing to a payload
	/// </summary>
	/// <typeparam name="TPayload">The type of payload being handled</typeparam>
	public interface ISubscriberHandler<in TPayload> : IDomain
		where TPayload : IPayload
	{
		/// <summary>
		/// Handles subscribing to a payload
		/// </summary>
		/// <param name="payload">The payload</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>An awaitable task</returns>
		Task HandleAsync(TPayload payload, CancellationToken cancellationToken);
	}
}
