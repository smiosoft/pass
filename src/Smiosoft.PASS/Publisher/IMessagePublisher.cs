using System.Threading.Tasks;

namespace Smiosoft.PASS.Publisher
{
	public interface IMessagePublisher<TMessage>
	{
		Task PublishAsync(TMessage message);
	}
}
