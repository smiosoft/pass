using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
    internal partial class ExampleTopicPublisher
    {
        internal class ServiceBus : Smiosoft.PASS.ServiceBus.Publisher.TopicPublisher<ServiceBusExampleTopicPayload>
        {
            public ServiceBus() : base("localhost", TOPIC_NAME)
            { }
        }
    }
}
