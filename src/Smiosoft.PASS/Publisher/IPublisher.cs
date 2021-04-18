namespace Smiosoft.PASS.Publisher
{
	public interface IPublisher<TClient, TMessage> : IBasePublisher, IClient<TClient>, IMessagePublisher<TMessage>
		where TClient : class
		where TMessage : class
	{ }
}
