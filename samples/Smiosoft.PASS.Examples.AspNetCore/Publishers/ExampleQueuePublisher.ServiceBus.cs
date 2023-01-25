using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
    internal partial class ExampleQueuePublisher
    {
        internal class ServiceBus : Smiosoft.PASS.ServiceBus.Publisher.QueuePublisher<ServiceBusExampleQueuePayload>
        {
            public ServiceBus() : base("localhost", QUEUE_NAME)
            { }
        }
    }
}
