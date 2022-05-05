using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Examples.AspNetCore.Payloads
{
	public class ServiceBusExampleQueuePayload : IPayload
	{
		public string Message { get; set; } = "Default message from Service Bus";
	}
}
