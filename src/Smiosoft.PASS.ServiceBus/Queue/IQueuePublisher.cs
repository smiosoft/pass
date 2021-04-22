using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.ServiceBus.Queue
{
	public interface IQueuePublisher : IBasePublisher
	{ }

	public interface IQueuePublisher<TMessage> : IQueuePublisher, IPublisher<IQueueClient, TMessage>
		where TMessage : class
	{ }
}
