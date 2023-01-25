using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.ServiceBus.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers
{
    public static class Publishers
    {
        public class QueuePublisherOne : QueuePublisher<Payloads.DummyPayloadOne>
        {
            private readonly ServiceBusClient? _client;

            public QueuePublisherOne(string connectionString, string queueName)
                : base(connectionString, queueName)
            { }

            public QueuePublisherOne(string connectionString, string queueName, ServiceBusClient? client)
                : base(connectionString, queueName)
            {
                _client = client;
            }

            protected override ServiceBusClient CreateDefaultClient()
            {
                return _client ?? base.CreateDefaultClient();
            }
        }

        public class TopicPublisherOne : TopicPublisher<Payloads.DummyPayloadOne>
        {
            private readonly ServiceBusClient? _client;

            public TopicPublisherOne(string connectionString, string topicPath)
                : base(connectionString, topicPath)
            { }

            public TopicPublisherOne(string connectionString, string topicPath, ServiceBusClient? client)
                : base(connectionString, topicPath)
            {
                _client = client;
            }

            protected override ServiceBusClient CreateDefaultClient()
            {
                return _client ?? base.CreateDefaultClient();
            }
        }
    }
}
