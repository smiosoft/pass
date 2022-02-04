using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class ServiceBusTopicSubscriber<TMessage> : ServiceBusSubscriberBase<TMessage>
		where TMessage : class
	{
		protected string TopicName { get; }
		protected string SubscriptionName { get; }

		protected ServiceBusTopicSubscriber(string connectionString, string topicName, string subscriptionName)
			: base(connectionString)
		{
			if (string.IsNullOrWhiteSpace(topicName))
			{
				throw new ArgumentNullException(nameof(topicName));
			}

			if (string.IsNullOrWhiteSpace(subscriptionName))
			{
				throw new ArgumentNullException(nameof(subscriptionName));
			}

			TopicName = topicName;
			SubscriptionName = subscriptionName;
		}

		public override Task RegisterAsync()
		{
			return SetupProcessorAsync();
		}

		protected override ServiceBusProcessor CreateProcessor()
		{
			return Client.CreateProcessor(topicName: TopicName, subscriptionName: SubscriptionName);
		}
	}
}
