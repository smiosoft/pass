using NSubstitute;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
    public partial class TopicPublisherTests
    {
        private readonly IConnectionFactory _mockConnectionFactory;
        private readonly IConnection _mockConnection;
        private readonly IModel _mockChannel;
        private readonly Publishers.TopicPublisherOne _sut;

        public TopicPublisherTests()
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

            _sut = new Publishers.TopicPublisherOne("local-tests", "tests", "unit.test", _mockConnectionFactory);
        }
    }
}
