using Azure.Messaging.ServiceBus;
using NSubstitute;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
    public partial class QueuePublisherTests
    {
        private readonly ServiceBusClient _mockServiceBusClient;
        private readonly ServiceBusSender _mockServiceBusSender;
        private readonly Publishers.QueuePublisherOne _sut;

        public QueuePublisherTests()
        {
            _mockServiceBusClient = Substitute.For<ServiceBusClient>();
            _mockServiceBusSender = Substitute.For<ServiceBusSender>();

            _mockServiceBusClient
                .CreateSender(Arg.Any<string>())
                .Returns(_mockServiceBusSender);

            _sut = new Publishers.QueuePublisherOne("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue", _mockServiceBusClient);
        }
    }
}
