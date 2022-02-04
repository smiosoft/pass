using System.Threading.Tasks;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public class ServiceBusQueuePublisher<TMessage> : ServiceBusPublisherBase<TMessage>
		where TMessage : class
	{
		protected string QueueName { get; }

		public ServiceBusQueuePublisher(string connectionString, string queueName)
			: base(connectionString)
		{
			QueueName = queueName;
		}

		public override Task PublishAsync(TMessage message)
		{
			return SendMessageAsync(QueueName, message);
		}
	}
}
