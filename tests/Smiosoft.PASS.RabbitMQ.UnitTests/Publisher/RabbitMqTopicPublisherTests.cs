using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
	public partial class RabbitMqTopicPublisherTests
	{
		private readonly Mock<IConnectionFactory> _mockConnectionFactory;
		private readonly Mock<IConnection> _mockConnection;
		private readonly Mock<IModel> _mockChannel;
		private readonly RabbitMqTopicPublisher<DummyTestMessageOne> _sut;

		public RabbitMqTopicPublisherTests()
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

			_sut = new MessageOneTopicPublisher("local-tests", "tests", "unit.test", _mockConnectionFactory.Object);
		}
	}
}
