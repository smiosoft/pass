using Microsoft.Azure.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.Topic;
using Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Publishers;
using Smiosoft.PASS.UnitTests.Helpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Topic
{
	public partial class TopicPublisherTests
	{
		private readonly Mock<ITopicClient> _mockTopicClient;
		private readonly TopicPublisher<DummyTestMessageOne> _sut;

		public TopicPublisherTests()
		{
			_mockTopicClient = new Mock<ITopicClient>();

			_sut = new MessageOneTopicPublisher(_mockTopicClient.Object);
		}
	}
}
