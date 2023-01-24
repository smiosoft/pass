using System.Collections.Generic;
using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class TopicSubscriberTests
    {
        private readonly Mock<IConnectionFactory> _mockConnectionFactory;
        private readonly Mock<IConnection> _mockConnection;
        private readonly Mock<IModel> _mockChannel;
        private readonly Subscribers.TopicSubscriberOne _sut;

        public TopicSubscriberTests()
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

            _mockChannel
                .Setup(_ => _.QueueDeclare(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(new QueueDeclareOk("test-queue", 1, 1));

            _sut = new Subscribers.TopicSubscriberOne("local-tests", "tests", "test", "unit.test", _mockConnectionFactory.Object);
        }
    }
}
