using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.RabbitMQ.Topic
{
	public interface ITopicPublisher : IBasePublisher
	{ }

	public interface ITopicPublisher<TMessage> : ITopicPublisher, IPublisher<TMessage>
		where TMessage : class
	{ }
}
