using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public interface IServiceBusSubscriber : IBaseSubscriber
	{ }

	public interface IServiceBusSubscriber<TMessage> : IServiceBusSubscriber, ISubscriber<TMessage>
		where TMessage : class
	{ }
}
