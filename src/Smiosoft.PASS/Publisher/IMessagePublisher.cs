using System;
using System.Threading.Tasks;

namespace Smiosoft.PASS.Publisher
{
	public interface IMessagePublisher<TMessage>
	{
		Task OnExceptionAsync(Exception exception);
		Task PublishAsync(TMessage message);
	}
}
