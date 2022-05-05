namespace Smiosoft.PASS.ServiceBus
{
	/// <summary>
	/// Extensions to register everything PASS for Service Bus
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Scope registration to only Service Bus related PASS types
		/// </summary>
		/// <param name="source">PASS service configuration</param>
		/// <returns>PASS service configuration</returns>
		public static PassServiceConfiguration WithServiceBus(this PassServiceConfiguration source)
		{
			source.WithEvaluator((type) => typeof(IServiceBus).IsAssignableFrom(type));
			return source;
		}
	}
}
