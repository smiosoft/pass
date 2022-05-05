using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Subscribers
{
	internal partial class ExampleQueueSubscriber
	{
		internal class ServiceBus : Smiosoft.PASS.ServiceBus.Subscriber.QueueSubscriber<ServiceBusExampleQueuePayload>
		{
			public ServiceBus() : base("localhost", QUEUE_NAME)
			{ }

			public override Task OnExceptionAsync(Exception exception)
			{
				Log.Error(exception, "An error occured in the Service Bus queue [{queue}] subscriber.", QUEUE_NAME);
				return Task.CompletedTask;
			}

			public override Task OnReceivedAsync(ServiceBusExampleQueuePayload payload, CancellationToken cancellationToken = default)
			{
				Log.Information("Received a payload on the Service Bus queue [{queue}]: {message}", QUEUE_NAME, payload.Message);
				return Task.CompletedTask;
			}
		}
	}
}
