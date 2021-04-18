using Microsoft.Extensions.DependencyInjection;

namespace Smiosoft.PASS.ServiceBuss.Configuration
{
	public class ServiceBusOptions
	{
		private readonly IServiceCollection _services;

		public ServiceBusOptions(IServiceCollection services)
		{
			_services = services;
		}
	}
}
