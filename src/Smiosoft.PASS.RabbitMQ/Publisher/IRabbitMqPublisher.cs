using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public interface IRabbitMqPublisher : IBasePublisher
	{ }

	public interface IRabbitMqPublisher<TMessage> : IRabbitMqPublisher, IPublisher<TMessage>
		where TMessage : class
	{ }
}
