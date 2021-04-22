using Microsoft.Azure.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.Topic;
using Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Subscribers;
using Smiosoft.PASS.UnitTests.Helpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Topic
{
	public partial class TopicSubscriberTests
	{
		private readonly Mock<ISubscriptionClient> _mockSubscriptionClient;
		private readonly TopicSubscriber<DummyTestMessageOne> _sut;

		public TopicSubscriberTests()
		{
			_mockSubscriptionClient = new Mock<ISubscriptionClient>();

			_sut = new MessageOneTopicSubscriber(_mockSubscriptionClient.Object);
		}
	}
}
