using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public interface IQueuePublisher : IBasePublisher
	{ }

	public interface IQueuePublisher<TMessage> : IQueuePublisher, IPublisher<TMessage>
		where TMessage : class
	{ }
}
