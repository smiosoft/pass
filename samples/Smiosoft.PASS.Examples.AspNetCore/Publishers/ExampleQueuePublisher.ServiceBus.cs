using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
    internal partial class ExampleQueuePublisher
    {
        internal class ServiceBus : Smiosoft.PASS.ServiceBus.Publisher.QueuePublisher<ServiceBusExampleQueuePayload>
        {
            public ServiceBus() : base("localhost", QUEUE_NAME)
            { }

            public override Task OnExceptionAsync(Exception exception)
            {
                Log.Error(exception, "An error occured whilst trying to publish a payload on the Service Bus queue [{queue}].", QUEUE_NAME);
                throw new InvalidOperationException("An error occured whilst trying to publish. Check the inner exception.", exception);
            }
        }
    }
}
