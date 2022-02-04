using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class ServiceBusQueueSubscriber<TMessage> : ServiceBusSubscriberBase<TMessage>
		where TMessage : class
	{
		protected string QueueName { get; }

		protected ServiceBusQueueSubscriber(string connectionString, string queueName)
			: base(connectionString)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			QueueName = queueName;
		}

		public override Task RegisterAsync()
		{
			return SetupProcessorAsync();
		}

		protected override ServiceBusProcessor CreateProcessor()
		{
			return Client.CreateProcessor(queueName: QueueName);
		}
	}
}
