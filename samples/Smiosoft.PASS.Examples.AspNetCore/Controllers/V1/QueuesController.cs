using Microsoft.AspNetCore.Mvc;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Controllers.V1
{
	public class QueuesController : V1ControllerBase
	{
		public QueuesController(IPass pass) : base(pass)
		{ }

		[HttpPost("rabbitmq")]
		public async Task<IActionResult> PublishWithRabbitMq([FromBody] RabbitMqExampleQueuePayload payload)
		{
			await Pass.PublishAsync(payload);
			return Ok();
		}

		[HttpPost("servicebus")]
		public async Task<IActionResult> PublishWithServiceBus([FromBody] ServiceBusExampleQueuePayload payload)
		{
			await Pass.PublishAsync(payload);
			return Ok();
		}
	}
}
