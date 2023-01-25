using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers
{
    public static class Publishers
    {
        public class QueuePublisherOne : QueuePublisher<Payloads.DummyPayloadOne>
        {
            public QueuePublisherOne(QueuePublisherOptions options, IConnectionFactory factory)
                : base(options, factory)
            { }

            public QueuePublisherOne(string hostName, string queueName, IConnectionFactory factory)
                : this(new QueuePublisherOptions() { HostName = hostName, QueueName = queueName }, factory)
            { }
        }

        public class TopicPublisherOne : TopicPublisher<Payloads.DummyPayloadOne>
        {
            public TopicPublisherOne(TopicPublisherOptions options, IConnectionFactory factory)
                : base(options, factory)
            { }

            public TopicPublisherOne(string hostName, string exchangeName, string routingKey, IConnectionFactory factory)
                : this(new TopicPublisherOptions() { HostName = hostName, ExchangeName = exchangeName, RoutingKey = routingKey }, factory)
            { }
        }
    }
}
