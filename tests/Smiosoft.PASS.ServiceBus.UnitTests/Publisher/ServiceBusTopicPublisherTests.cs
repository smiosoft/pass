using Smiosoft.PASS.ServiceBus.Publisher;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Publishers;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
	public partial class ServiceBusTopicPublisherTests
	{
		private readonly ServiceBusTopicPublisher<DummyTestMessageOne> _sut;

		public ServiceBusTopicPublisherTests()
		{
			_sut = new MessageOneTopicPublisher("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic");
		}
	}
}
