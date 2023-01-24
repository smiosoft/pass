using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        private readonly Mock<IConnectionFactory> _mockConnectionFactory;
        private readonly Mock<IConnection> _mockConnection;
        private readonly Mock<IModel> _mockChannel;
        private readonly Subscribers.QueueSubscriberOne _sut;

        public QueueSubscriberTests()
        {
            _mockConnectionFactory = new Mock<IConnectionFactory>();
            _mockConnection = new Mock<IConnection>();
            _mockChannel = new Mock<IModel>();

            _mockConnectionFactory
                .Setup(_ => _.CreateConnection())
                .Returns(_mockConnection.Object);

            _mockConnection
                .Setup(_ => _.CreateModel())
                .Returns(_mockChannel.Object);

            _sut = new Subscribers.QueueSubscriberOne("local-tests", "test-queue", _mockConnectionFactory.Object);
        }
    }
}
