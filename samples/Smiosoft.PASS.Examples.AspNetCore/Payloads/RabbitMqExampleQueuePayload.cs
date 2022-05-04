using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Examples.AspNetCore.Payloads
{
	public partial class RabbitMqExampleQueuePayload : IPayload
	{
		public string Message { get; set; } = string.Empty;
	}


}
