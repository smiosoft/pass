using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Configuration
{
	public class ServiceBusOptions
	{
		public string ConnectionString { get; }

		[JsonConstructor]
		public ServiceBusOptions(string connectionString)
		{
			ConnectionString = connectionString;
		}
	}
}
