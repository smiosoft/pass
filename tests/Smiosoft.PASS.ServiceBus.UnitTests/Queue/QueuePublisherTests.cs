using Microsoft.Azure.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.Queue;
using Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Publishers;
using Smiosoft.PASS.UnitTests.Helpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Queue
{
	public partial class QueuePublisherTests
	{
		private readonly Mock<IQueueClient> _mockQueueClient;
		private readonly QueuePublisher<DummyTestMessageOne> _sut;

		public QueuePublisherTests()
		{
			_mockQueueClient = new Mock<IQueueClient>();

			_sut = new MessageOneQueuePublisher(_mockQueueClient.Object);
		}
	}
}
