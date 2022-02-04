using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public interface IServiceBusPublisher : IBasePublisher
	{ }

	public interface IServiceBusPublisher<TMessage> : IServiceBusPublisher, IPublisher<TMessage>
		where TMessage : class
	{ }
}
