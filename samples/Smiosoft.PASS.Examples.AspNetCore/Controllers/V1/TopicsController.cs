using Microsoft.AspNetCore.Mvc;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Controllers.V1
{
    public class TopicsController : V1ControllerBase
    {
        public TopicsController(IPass pass) : base(pass)
        { }

        [HttpPost("rabbitmq")]
        public async Task<IActionResult> PublishWithRabbitMq([FromBody] RabbitMqExampleTopicPayload payload)
        {
            await Pass.PublishAsync(payload);
            return Ok();
        }

        [HttpPost("servicebus")]
        public async Task<IActionResult> PublishWithServiceBus([FromBody] ServiceBusExampleTopicPayload payload)
        {
            await Pass.PublishAsync(payload);
            return Ok();
        }
    }
}
