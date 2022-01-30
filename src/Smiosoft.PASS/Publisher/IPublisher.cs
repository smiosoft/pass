namespace Smiosoft.PASS.Publisher
{
	public interface IPublisher<TMessage> : IBasePublisher, IMessagePublisher<TMessage>
		where TMessage : class
	{ }

	public interface IPublisher<TClient, TMessage> : IClient<TClient>, IPublisher<TMessage>
		where TClient : class
		where TMessage : class
	{ }
}
