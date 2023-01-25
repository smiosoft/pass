using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
    internal partial class ExampleTopicPublisher
    {
        internal class RabbitMq : Smiosoft.PASS.RabbitMQ.Publisher.TopicPublisher<RabbitMqExampleTopicPayload>
        {
            public RabbitMq() : base("localhost", TOPIC_NAME, routingKey: "default")
            { }
        }
    }
}
