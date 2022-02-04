using Smiosoft.PASS.ServiceBus.Publisher;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Publishers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
	public partial class ServiceBusQueuePublisherTests
	{
		private readonly ServiceBusQueuePublisher<DummyTestMessageOne> _sut;

		public ServiceBusQueuePublisherTests()
		{
			_sut = new MessageOneQueuePublisher("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue");
		}
	}
}
