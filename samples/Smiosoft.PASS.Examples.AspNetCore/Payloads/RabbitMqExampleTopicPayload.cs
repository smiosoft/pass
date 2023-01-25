using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.Examples.AspNetCore.Payloads
{
    public class RabbitMqExampleTopicPayload : IPayload
    {
        public string Message { get; set; } = "Default message from RabbitMQ";
    }
}
