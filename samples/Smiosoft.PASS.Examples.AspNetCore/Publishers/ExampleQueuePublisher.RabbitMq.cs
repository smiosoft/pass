using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
    internal partial class ExampleQueuePublisher
    {
        internal class RabbitMq : Smiosoft.PASS.RabbitMQ.Publisher.QueuePublisher<RabbitMqExampleQueuePayload>
        {
            public RabbitMq() : base("localhost", QUEUE_NAME)
            { }
        }
    }
}
