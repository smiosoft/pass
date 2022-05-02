using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusPublisherOptions : ServiceBusOptions
	{
		[JsonConstructor]
		public ServiceBusPublisherOptions(string connectionString)
			: base(connectionString)
		{ }
	}
}
