using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
    internal partial class ExampleTopicPublisher
    {
        internal class ServiceBus : Smiosoft.PASS.ServiceBus.Publisher.TopicPublisher<ServiceBusExampleTopicPayload>
        {
            public ServiceBus() : base("localhost", TOPIC_NAME)
            { }

            public override Task OnExceptionAsync(Exception exception)
            {
                Log.Error(exception, "An error occured whilst trying to publish a payload on the Service Bus topic [{topic}].", TOPIC_NAME);
                throw new InvalidOperationException("An error occured whilst trying to publish. Check the inner exception.", exception);
            }
        }
    }
}
