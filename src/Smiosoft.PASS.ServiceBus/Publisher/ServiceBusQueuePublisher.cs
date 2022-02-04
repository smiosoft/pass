using System;
using System.Threading.Tasks;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class ServiceBusQueuePublisher<TMessage> : ServiceBusPublisherBase<TMessage>
		where TMessage : class
	{
		protected string QueueName { get; }

		protected ServiceBusQueuePublisher(string connectionString, string queueName)
			: base(connectionString)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			QueueName = queueName;
		}

		public override Task PublishAsync(TMessage message)
		{
			return SendMessageAsync(QueueName, message);
		}
	}
}
