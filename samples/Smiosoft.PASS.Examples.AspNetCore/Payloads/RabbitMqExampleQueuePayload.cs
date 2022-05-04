namespace Smiosoft.PASS.Examples.AspNetCore.Payloads
{
	public partial class ExampleQueuePayload : IPayload
	{
		public string Message { get; set; } = string.Empty;
	}


}
