using System;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
    public abstract class TopicSubscriber<TPayload> : SubscriberBase<TPayload>
        where TPayload : IPayload
    {
        protected TopicSubscriberOptions Options { get; }

        protected TopicSubscriber(TopicSubscriberOptions topicSubscriberOptions)
            : base(topicSubscriberOptions)
        {
            Options = topicSubscriberOptions ?? throw new ArgumentNullException(nameof(topicSubscriberOptions));
        }

        protected TopicSubscriber(TopicSubscriberOptions topicSubscriberOptions, ServiceBusClient client)
            : base(topicSubscriberOptions, client)
        {
            Options = topicSubscriberOptions ?? throw new ArgumentNullException(nameof(topicSubscriberOptions));
        }

        protected TopicSubscriber(string connectionString, string topicName, string subscriptionName)
            : this(new TopicSubscriberOptions() { ConnectionString = connectionString, TopicName = topicName, SubscriptionName = subscriptionName })
        { }

        protected override ServiceBusProcessor CreateDefaultProcessor()
        {
            return Client.CreateProcessor(topicName: Options.TopicName, subscriptionName: Options.SubscriptionName);
        }
    }
}
