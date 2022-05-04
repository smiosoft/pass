using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Subscribers
{
	internal partial class ExampleTopicSubscriber
	{
		internal class RabbitMq : Smiosoft.PASS.RabbitMQ.Subscriber.TopicSubscriber<RabbitMqExampleTopicPayload>
		{
			public RabbitMq() : base("localhost", TOPIC_NAME, routingKey: "default")
			{ }

			public override Task OnExceptionAsync(Exception exception)
			{
				Log.Error(exception, "An error occured in the RabbitMQ topic [{topic}] subscriber.", TOPIC_NAME);
				return Task.CompletedTask;
			}

			public override Task OnRecivedAsync(RabbitMqExampleTopicPayload payload, CancellationToken cancellationToken = default)
			{
				Log.Information("Recieved a payload on the RabbitMQ topic [{topic}]: {message}", TOPIC_NAME, payload.Message);
				return Task.CompletedTask;
			}
		}
	}
}
