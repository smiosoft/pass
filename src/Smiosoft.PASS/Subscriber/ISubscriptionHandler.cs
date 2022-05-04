using System;
using System.Threading;
using System.Threading.Tasks;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Subscriber
{
	/// <summary>
	/// Defines a handler for a payload subscription
	/// </summary>
	/// <typeparam name="TPayload">The type of payload being handled</typeparam>
	public interface ISubscriptionHandler<in TPayload> : IListener, IDomain
		where TPayload : IPayload
	{
		/// <summary>
		/// Triggered when an exception occurs
		/// </summary>
		/// <param name="exception">Exception</param>
		/// <returns>An awaitable task</returns>
		Task OnExceptionAsync(Exception exception);

		/// <summary>
		/// Triggered when a payload is recieved
		/// </summary>
		/// <param name="payload">Payload object</param>
		/// <param name="cancellationToken">Optional cancellation token</param>
		/// <returns>An awaitable task</returns>
		Task OnRecivedAsync(TPayload payload, CancellationToken cancellationToken = default);
	}
}
