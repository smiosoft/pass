using NSubstitute;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
    public partial class QueuePublisherTests
    {
        private readonly IConnectionFactory _mockConnectionFactory;
        private readonly IConnection _mockConnection;
        private readonly IModel _mockChannel;
        private readonly Publishers.QueuePublisherOne _sut;

        public QueuePublisherTests()
        {
            _mockConnectionFactory = Substitute.For<IConnectionFactory>();
            _mockConnection = Substitute.For<IConnection>();
            _mockChannel = Substitute.For<IModel>();

            _mockConnectionFactory
                .CreateConnection()
                .Returns(_mockConnection);

            _mockConnection
                .CreateModel()
                .Returns(_mockChannel);

            _mockChannel
                .CreateBasicProperties()
                .Returns(Substitute.For<IBasicProperties>());

            _sut = new Publishers.QueuePublisherOne("local-tests", "test-queue", _mockConnectionFactory);
        }
    }
}
