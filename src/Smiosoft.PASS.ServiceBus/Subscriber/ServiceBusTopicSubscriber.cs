using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class ServiceBusTopicSubscriber<TMessage> : ServiceBusSubscriberBase<TMessage>
		where TMessage : class
	{
		protected ServiceBusTopicSubscriberOptions TopicSubscriberOptions { get; }

		protected ServiceBusTopicSubscriber(ServiceBusTopicSubscriberOptions topicSubscriberOptions)
			: base(topicSubscriberOptions)
		{
			TopicSubscriberOptions = topicSubscriberOptions ?? throw new ArgumentNullException(nameof(topicSubscriberOptions));
		}

		public override Task RegisterAsync()
		{
			return SetupProcessorAsync();
		}

		protected override ServiceBusProcessor CreateProcessor()
		{
			return Client.CreateProcessor(
				topicName: TopicSubscriberOptions.TopicName,
				subscriptionName: TopicSubscriberOptions.SubscriptionName);
		}
	}
}
