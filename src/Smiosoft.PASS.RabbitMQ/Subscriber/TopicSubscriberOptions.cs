namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
    public class TopicSubscriberOptions : SubscriberOptions
    {
        public string ExchangeName { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
        public string RoutingKey { get; set; } = string.Empty;
    }
}
