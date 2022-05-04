namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public class QueuePublisherOptions : PublisherOptions
	{
		public string QueueName { get; set; } = string.Empty;
	}
}
