using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.ServiceBus.Topic
{
	public interface ITopicSubscriber : IBaseSubscriber
	{ }

	public interface ITopicSubscriber<TMessage> : ITopicSubscriber, ISubscriber<ISubscriptionClient, TMessage>
		where TMessage : class
	{ }
}
