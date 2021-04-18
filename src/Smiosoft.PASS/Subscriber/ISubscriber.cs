namespace Smiosoft.PASS.Subscriber
{
	public interface ISubscriber<TClient, TMessage> : IBaseSubscriber, IClient<TClient>, IMessageSubscriber<TMessage>
		where TClient : class
		where TMessage : class
	{ }
}
