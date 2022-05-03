using System.Threading;
using System.Threading.Tasks;

namespace Smiosoft.PASS
{
	/// <summary>
	/// Send a payload through the pass pipeline to be handled by a single handler
	/// </summary>
	public interface ISender
	{
		/// <summary>
		/// Asynchronously send a payload to a single handler
		/// </summary>
		/// <param name="payload">Payload object</param>
		/// <param name="cancellationToken">Optional cancellation token</param>
		/// <returns>An awaitable task</returns>
		Task SendAsync(IPayload payload, CancellationToken cancellationToken = default);
	}
}
