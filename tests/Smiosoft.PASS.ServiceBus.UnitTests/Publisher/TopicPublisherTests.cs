using Azure.Messaging.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
    public partial class TopicPublisherTests
    {
        private readonly Mock<ServiceBusClient> _mockServiceBusClient;
        private readonly Mock<ServiceBusSender> _mockServiceBusSender;
        private readonly Publishers.TopicPublisherOne _sut;

        public TopicPublisherTests()
        {
            _mockServiceBusClient = new Mock<ServiceBusClient>();
            _mockServiceBusSender = new Mock<ServiceBusSender>();

            _mockServiceBusClient
                .Setup(_ => _.CreateSender(It.IsAny<string>()))
                .Returns(_mockServiceBusSender.Object);

            _sut = new Publishers.TopicPublisherOne("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic", _mockServiceBusClient.Object);
        }
    }
}
