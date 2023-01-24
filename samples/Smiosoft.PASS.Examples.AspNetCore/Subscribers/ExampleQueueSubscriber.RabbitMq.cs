using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Subscribers
{
    internal partial class ExampleQueueSubscriber
    {
        internal class RabbitMq : Smiosoft.PASS.RabbitMQ.Subscriber.QueueSubscriber<RabbitMqExampleQueuePayload>
        {
            public RabbitMq() : base("localhost", QUEUE_NAME)
            { }

            public override Task OnExceptionAsync(Exception exception)
            {
                Log.Error(exception, "An error occured in the RabbitMQ queue [{queue}] subscriber.", QUEUE_NAME);
                return Task.CompletedTask;
            }

            public override Task OnReceivedAsync(RabbitMqExampleQueuePayload payload, CancellationToken cancellationToken = default)
            {
                Log.Information("Received a payload on the RabbitMQ queue [{queue}]: {message}", QUEUE_NAME, payload.Message);
                return Task.CompletedTask;
            }
        }
    }
}
