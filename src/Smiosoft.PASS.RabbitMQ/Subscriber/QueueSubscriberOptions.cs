namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
    public class QueueSubscriberOptions : SubscriberOptions
    {
        public string QueueName { get; set; } = string.Empty;
    }
}
