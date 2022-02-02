using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.RabbitMQ.Queue
{
	public interface IQueuePublisher : IBasePublisher
	{ }

	public interface IQueuePublisher<TMessage> : IQueuePublisher, IPublisher<TMessage>
		where TMessage : class
	{ }
}
