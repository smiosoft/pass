namespace Smiosoft.PASS.ServiceBus.Subscriber
{
    public class TopicSubscriberOptions : SubscriberOptions
    {
        public string TopicName { get; set; } = string.Empty;
        public string SubscriptionName { get; set; } = string.Empty;
    }
}
