using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.RabbitMQ.Queue
{
	public interface IQueueSubscriber : IBaseSubscriber
	{ }

	public interface IQueueSubscriber<TMessage> : IQueueSubscriber, ISubscriber<TMessage>
		where TMessage : class
	{ }
}
