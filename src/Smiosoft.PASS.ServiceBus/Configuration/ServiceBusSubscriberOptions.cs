using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusSubscriberOptions : ServiceBusOptions
	{
		[JsonConstructor]
		public ServiceBusSubscriberOptions(string connectionString)
			: base(connectionString)
		{ }
	}
}
