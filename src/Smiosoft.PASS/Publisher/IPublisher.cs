using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Publisher
{
	/// <summary>
	/// Send a payload through the pass pipeline to be handled by a single <see cref="IPublishingHandler{TPayload}" />
	/// </summary>
	public interface IPublisher : IDomain
	{
		/// <summary>
		/// Asynchronously publish a payload via a single <see cref="IPublishingHandler{TPayload}" />
		/// </summary>
		/// <param name="payload">Payload object</param>
		/// <param name="cancellationToken">Optional cancellation token</param>
		/// <returns>An awaitable task</returns>
		Task PublishAsync(IPayload payload, CancellationToken cancellationToken = default);
	}
}
