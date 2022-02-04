using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
	public interface IRabbitMqSubscriber : IBaseSubscriber
	{ }

	public interface IRabbitMqSubscriber<TMessage> : IRabbitMqSubscriber, ISubscriber<TMessage>
		where TMessage : class
	{ }
}
