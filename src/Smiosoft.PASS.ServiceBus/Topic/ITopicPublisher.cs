using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.ServiceBus.Topic
{
	public interface ITopicPublisher : IBasePublisher
	{ }

	public interface ITopicPublisher<TMessage> : ITopicPublisher, IPublisher<ITopicClient, TMessage>
		where TMessage : class
	{ }
}