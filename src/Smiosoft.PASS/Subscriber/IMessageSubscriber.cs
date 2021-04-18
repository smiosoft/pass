using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smiosoft.PASS.Subscriber
{
	public interface IMessageSubscriber<TMessage>
		where TMessage : class
	{
		Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);
		Task OnExceptionAsync(Exception exception);
	}
}
