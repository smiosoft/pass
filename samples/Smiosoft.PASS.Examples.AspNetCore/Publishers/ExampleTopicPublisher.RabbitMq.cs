using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
	internal partial class ExampleTopicPublisher
	{
		internal class RabbitMq : Smiosoft.PASS.RabbitMQ.Publisher.TopicPublisher<RabbitMqExampleTopicPayload>
		{
			public RabbitMq() : base("localhost", TOPIC_NAME, routingKey: "default")
			{ }

			public override Task OnExceptionAsync(Exception exception)
			{
				Log.Error(exception, "An error occured whilst trying to publish a payload on the RabbitMQ topic [{topic}].", TOPIC_NAME);
				throw new InvalidOperationException("An error occured whilst trying to publish. Check the inner exception.", exception);
			}
		}
	}
}
