using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Subscribers
{
	internal partial class ExampleQueueSubscriber
	{
		internal class RabbitMq : Smiosoft.PASS.RabbitMQ.Subscriber.QueueSubscriber<RabbitMqExampleQueuePayload>
		{
			public RabbitMq() : base("localhost", QUEUE_NAME)
			{ }

			protected override Task OnExceptionAsync(Exception exception)
			{
				throw new NotImplementedException();
			}

			protected override Task OnRecivedAsync(RabbitMqExampleQueuePayload payload, CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}
		}
	}
}
