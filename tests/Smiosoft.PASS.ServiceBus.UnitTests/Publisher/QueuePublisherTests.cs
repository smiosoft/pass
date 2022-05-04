using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
	public partial class QueuePublisherTests
	{
		private readonly Publishers.QueuePublisherOne _sut;

		public QueuePublisherTests()
		{
			_sut = new Publishers.QueuePublisherOne("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue");
		}
	}
}
