using Azure.Messaging.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.Subscriber;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
	public partial class ServiceBusTopicSubscriberTests
	{
		private readonly Mock<ServiceBusProcessor> _mockServiceBusProcessor;
		private readonly ServiceBusTopicSubscriber<DummyTestMessageOne> _sut;

		public ServiceBusTopicSubscriberTests()
		{
			_mockServiceBusProcessor = new Mock<ServiceBusProcessor>();

			_sut = new MessageOneTopicSubscriber(
				_mockServiceBusProcessor.Object,
				"Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***",
				"test-topic",
				"test-subscription");
		}
	}
}
