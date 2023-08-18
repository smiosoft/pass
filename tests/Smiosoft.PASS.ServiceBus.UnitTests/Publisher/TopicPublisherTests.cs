using Azure.Messaging.ServiceBus;
using NSubstitute;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
    public partial class TopicPublisherTests
    {
        private readonly ServiceBusClient _mockServiceBusClient;
        private readonly ServiceBusSender _mockServiceBusSender;
        private readonly Publishers.TopicPublisherOne _sut;

        public TopicPublisherTests()
        {
            _mockServiceBusClient = Substitute.For<ServiceBusClient>();
            _mockServiceBusSender = Substitute.For<ServiceBusSender>();

            _mockServiceBusClient
                .CreateSender(Arg.Any<string>())
                .Returns(_mockServiceBusSender);

            _sut = new Publishers.TopicPublisherOne("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic", _mockServiceBusClient);
        }
    }
}
