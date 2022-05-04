using Serilog;
using Smiosoft.PASS.Examples.AspNetCore.Payloads;

namespace Smiosoft.PASS.Examples.AspNetCore.Publishers
{
	internal class RabbitMqExampleQueuePublisher : Smiosoft.PASS.RabbitMQ.Publisher.QueuePublisher<RabbitMqExampleQueuePayload>
	{
		public const string QUEUE_NAME = "EXAMPLE";

		public RabbitMqExampleQueuePublisher() : base("localhost", QUEUE_NAME)
		{ }

		public override Task OnExceptionAsync(Exception exception)
		{
			Log.Error(exception, "An error occured whilst trying to publish a payload on the RabbitMq queue [{queue}].", QUEUE_NAME);
			throw new InvalidOperationException("An error occured whilst trying to publish. Check the inner exception.", exception);
		}
	}
}
