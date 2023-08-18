using Azure.Messaging.ServiceBus;
using NSubstitute;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
    public partial class TopicSubscriberTests
    {
        private readonly ServiceBusProcessor _mockServiceBusProcessor;
        private readonly Subscribers.TopicSubscriberOne _sut;

        public TopicSubscriberTests()
        {
            _mockServiceBusProcessor = Substitute.For<ServiceBusProcessor>();

            _sut = new Subscribers.TopicSubscriberOne(
                "Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***",
                "test-topic",
                "test-subscription",
                _mockServiceBusProcessor);
        }
    }
}
