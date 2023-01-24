namespace Smiosoft.PASS.RabbitMQ.Publisher
{
    public class TopicPublisherOptions : PublisherOptions
    {
        public string ExchangeName { get; set; } = string.Empty;
        public string RoutingKey { get; set; } = string.Empty;
    }
}
