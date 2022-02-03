using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Queue;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Queue
{
	public partial class QueueSubscriberTests
	{
		private readonly Mock<IConnectionFactory> _mockConnectionFactory;
		private readonly Mock<IConnection> _mockConnection;
		private readonly Mock<IModel> _mockChannel;
		private readonly QueueSubscriber<DummyTestMessageOne> _sut;

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

			_sut = new MessageOneQueueSubscriber(_mockConnectionFactory.Object, "test-queue");
		}
	}
}
