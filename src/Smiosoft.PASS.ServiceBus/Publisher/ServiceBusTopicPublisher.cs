using System;
using System.Threading.Tasks;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class ServiceBusTopicPublisher<TMessage> : ServiceBusPublisherBase<TMessage>
		where TMessage : class
	{
		protected string TopicName { get; }

		protected ServiceBusTopicPublisher(string connectionString, string topicName)
			: base(connectionString)
		{
			if (string.IsNullOrWhiteSpace(topicName))
			{
				throw new ArgumentNullException(nameof(topicName));
			}

			TopicName = topicName;
		}

		public override Task PublishAsync(TMessage message)
		{
			return SendMessageAsync(TopicName, message);
		}
	}
}
