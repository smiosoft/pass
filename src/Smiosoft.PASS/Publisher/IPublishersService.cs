using System.Threading.Tasks;

namespace Smiosoft.PASS.Publisher
{
	public interface IPublishersService
	{
		Task PublishAsync<TMessage>(TMessage message) where TMessage : class;
		Task<bool> TryPublishAsync<TMessage>(TMessage message) where TMessage : class;
	}
}
