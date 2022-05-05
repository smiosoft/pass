using Azure.Messaging.ServiceBus;
using Moq;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
	public partial class QueueSubscriberTests
	{
		private readonly Mock<ServiceBusProcessor> _mockServiceBusProcessor;
		private readonly Subscribers.QueueSubscriberOne _sut;

		public QueueSubscriberTests()
		{
			_mockServiceBusProcessor = new Mock<ServiceBusProcessor>();

			_sut = new Subscribers.QueueSubscriberOne(
				"Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***",
				"test-queue",
				_mockServiceBusProcessor.Object);
		}
	}
}
