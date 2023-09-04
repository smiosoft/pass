using Azure.Messaging.ServiceBus;
using NSubstitute;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        private readonly ServiceBusProcessor _mockServiceBusProcessor;
        private readonly Subscribers.QueueSubscriberOne _sut;

        public QueueSubscriberTests()
        {
            _mockServiceBusProcessor = Substitute.For<ServiceBusProcessor>();

            _sut = new Subscribers.QueueSubscriberOne(
                "Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***",
                "test-queue",
                _mockServiceBusProcessor);
        }
    }
}
