namespace Smiosoft.PASS.Subscriber
{
	public interface ISubscriber<TMessage> : IBaseSubscriber, IMessageSubscriber<TMessage>
		where TMessage : class
	{ }

	public interface ISubscriber<TClient, TMessage> : IClient<TClient>, ISubscriber<TMessage>
		where TClient : class
		where TMessage : class
	{ }
}
