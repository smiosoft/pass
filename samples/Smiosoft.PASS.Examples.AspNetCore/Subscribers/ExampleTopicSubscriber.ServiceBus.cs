using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Subscribers
{
    internal partial class ExampleTopicSubscriber
    {
        internal class ServiceBus : Smiosoft.PASS.ServiceBus.Subscriber.TopicSubscriber<ServiceBusExampleTopicPayload>
        {
            public ServiceBus() : base("localhost", TOPIC_NAME, subscriptionName: "default")
            { }

            public override Task OnExceptionAsync(Exception exception)
            {
                Log.Error(exception, "An error occured in the Service Bus topic [{topic}] subscriber.", TOPIC_NAME);
                return Task.CompletedTask;
            }

            public override Task OnReceivedAsync(ServiceBusExampleTopicPayload payload, CancellationToken cancellationToken = default)
            {
                Log.Information("Received a payload on the Service Bus topic [{topic}]: {message}", TOPIC_NAME, payload.Message);
                return Task.CompletedTask;
            }
        }
    }
}
