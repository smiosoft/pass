using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Publisher
{
	/// <summary>
	/// Defines a handler for publishing a payload
	/// </summary>
	/// <typeparam name="TPayload">The type of payload being handled</typeparam>
	public interface IPublishingHandler<in TPayload> : IDomain
		where TPayload : IPayload
	{
		/// <summary>
		/// Handles publishing a payload
		/// </summary>
		/// <param name="payload">The payload</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>An awaitable task</returns>
		Task HandleAsync(TPayload payload, CancellationToken cancellationToken);
	}
}
