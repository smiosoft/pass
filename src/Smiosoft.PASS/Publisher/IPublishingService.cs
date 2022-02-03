using System.Threading.Tasks;

namespace Smiosoft.PASS.Publisher
{
	public interface IPublishingService
	{
		Task PublishAsync<TMessage>(TMessage message) where TMessage : class;
		Task<bool> TryPublishAsync<TMessage>(TMessage message) where TMessage : class;
	}
}
