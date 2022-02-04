using System.Threading.Tasks;

namespace Smiosoft.PASS.Subscriber
{
	public interface IBaseSubscriber
	{
		Task RegisterAsync();
	}
}
