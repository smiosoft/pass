using System.Collections.Generic;
using NSubstitute;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class TopicSubscriberTests
    {
        private readonly IConnectionFactory _mockConnectionFactory;
        private readonly IConnection _mockConnection;
        private readonly IModel _mockChannel;
        private readonly Subscribers.TopicSubscriberOne _sut;

        public TopicSubscriberTests()
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
                .QueueDeclare(Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<IDictionary<string, object>>())
                .Returns(new QueueDeclareOk("test-queue", 1, 1));

            _sut = new Subscribers.TopicSubscriberOne("local-tests", "tests", "test", "unit.test", _mockConnectionFactory);
        }
    }
}
