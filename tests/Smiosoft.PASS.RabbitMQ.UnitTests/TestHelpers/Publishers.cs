using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers
{
    public static class Publishers
    {
        public class QueuePublisherOne : QueuePublisher<Payloads.DummyPayloadOne>
        {
            private readonly IConnectionFactory? _factory;

            public QueuePublisherOne(string hostName, string queueName, IConnectionFactory? factory) : base(hostName, queueName)
            {
                _factory = factory;
            }

            protected override IConnectionFactory CreateDefaultConnectionFactory()
            {
                return _factory ?? base.CreateDefaultConnectionFactory();
            }
        }

        public class TopicPublisherOne : TopicPublisher<Payloads.DummyPayloadOne>
        {
            private readonly IConnectionFactory? _factory;

            public TopicPublisherOne(string hostName, string exchangeName, string routingKey, IConnectionFactory? factory) : base(hostName, exchangeName, routingKey)
            {
                _factory = factory;
            }

            protected override IConnectionFactory CreateDefaultConnectionFactory()
            {
                return _factory ?? base.CreateDefaultConnectionFactory();
            }
        }
    }
}
