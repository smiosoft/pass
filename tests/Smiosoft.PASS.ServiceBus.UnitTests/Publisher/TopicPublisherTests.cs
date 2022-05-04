using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
	public partial class TopicPublisherTests
	{
		private readonly Publishers.TopicPublisherOne _sut;

		public TopicPublisherTests()
		{
			_sut = new Publishers.TopicPublisherOne("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic");
		}
	}
}
