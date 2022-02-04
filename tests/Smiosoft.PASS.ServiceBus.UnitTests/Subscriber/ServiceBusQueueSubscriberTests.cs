using Azure.Messaging.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.Subscriber;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
	public partial class ServiceBusQueueSubscriberTests
	{
		private readonly Mock<ServiceBusProcessor> _mockServiceBusProcessor;
		private readonly ServiceBusQueueSubscriber<DummyTestMessageOne> _sut;

		public ServiceBusQueueSubscriberTests()
		{
			_mockServiceBusProcessor = new Mock<ServiceBusProcessor>();

			_sut = new MessageOneQueueSubscriber(
				_mockServiceBusProcessor.Object,
				"Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***",
				"test-queue");
		}
	}
}
