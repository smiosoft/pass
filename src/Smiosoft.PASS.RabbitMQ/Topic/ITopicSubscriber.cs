using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.RabbitMQ.Topic
{
	public interface ITopicSubscriber : IBaseSubscriber
	{ }

	public interface ITopicSubscriber<TMessage> : ITopicSubscriber, ISubscriber<TMessage>
		where TMessage : class
	{ }
}
