using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.ServiceBuss.Queue
{
	public interface IQueueSubscriber : IBaseSubscriber
	{ }

	public interface IQueueSubscriber<TMessage> : IQueueSubscriber, ISubscriber<IQueueClient, TMessage>
		where TMessage : class
	{ }
}
