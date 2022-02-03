using System.Collections.Generic;
using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Topic;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Topic
{
	public partial class TopicSubscriberTests
	{
		private readonly Mock<IConnectionFactory> _mockConnectionFactory;
		private readonly Mock<IConnection> _mockConnection;
		private readonly Mock<IModel> _mockChannel;
		private readonly TopicSubscriber<DummyTestMessageOne> _sut;

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

			_sut = new MessageOneTopicSubscriber(_mockConnectionFactory.Object, "tests", "unit.test");
		}
	}
}
