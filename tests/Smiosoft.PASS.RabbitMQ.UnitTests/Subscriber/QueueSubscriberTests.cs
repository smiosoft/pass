using NSubstitute;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        private readonly IConnectionFactory _mockConnectionFactory;
        private readonly IConnection _mockConnection;
        private readonly IModel _mockChannel;
        private readonly Subscribers.QueueSubscriberOne _sut;

        public QueueSubscriberTests()
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

            _sut = new Subscribers.QueueSubscriberOne("local-tests", "test-queue", _mockConnectionFactory);
        }
    }
}
