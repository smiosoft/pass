using Smiosoft.PASS.ServiceBus.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers
{
    public static class Publishers
    {
        public class QueuePublisherOne : QueuePublisher<Payloads.DummyPayloadOne>
        {
            public QueuePublisherOne(string connectionString, string queueName) : base(connectionString, queueName)
            { }
        }

        public class TopicPublisherOne : TopicPublisher<Payloads.DummyPayloadOne>
        {
            public TopicPublisherOne(string connectionString, string topicPath) : base(connectionString, topicPath)
            { }
        }
    }
}
