using Microsoft.Azure.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.Queue;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Queue
{
	public partial class QueueSubscriberTests
	{
		private readonly Mock<IQueueClient> _mockQueueClient;
		private readonly QueueSubscriber<DummyTestMessageOne> _sut;

		public QueueSubscriberTests()
		{
			_mockQueueClient = new Mock<IQueueClient>();

			_sut = new MessageOneQueueSubscriber(_mockQueueClient.Object);
		}
	}
}
