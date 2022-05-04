namespace Smiosoft.PASS.Examples.AspNetCore.Payloads
{
	public class RabbitMqExampleQueuePayload : IPayload
	{
		public string Message { get; set; } = string.Empty;
	}
}
