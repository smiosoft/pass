using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class ServiceBusTopicSubscriber<TMessage> : ServiceBusSubscriberBase<TMessage>
		where TMessage : class
	{
		protected ServiceBusTopicSubscriberOptions Options { get; }

		protected ServiceBusTopicSubscriber(ServiceBusTopicSubscriberOptions topicSubscriberOptions)
			: base(topicSubscriberOptions)
		{
			Options = topicSubscriberOptions ?? throw new ArgumentNullException(nameof(topicSubscriberOptions));
		}

		protected ServiceBusTopicSubscriber(string connectionString, string topicName, string subscriptionName)
			: this(new ServiceBusTopicSubscriberOptions(connectionString, topicName, subscriptionName))
		{ }

		public override Task RegisterAsync()
		{
			return SetupProcessorAsync();
		}

		protected override ServiceBusProcessor CreateProcessor()
		{
			return Client.CreateProcessor(
				topicName: Options.TopicName,
				subscriptionName: Options.SubscriptionName);
		}
	}
}
