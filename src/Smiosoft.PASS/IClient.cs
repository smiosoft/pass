namespace Smiosoft.PASS
{
	public interface IClient<TClient>
	{
		TClient Client { get; }
	}
}
