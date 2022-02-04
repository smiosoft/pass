using Smiosoft.PASS.ServiceBus.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Publishers
{
	public class MessageOneTopicPublisher : ServiceBusTopicPublisher<DummyTestMessageOne>
	{
		public MessageOneTopicPublisher(string connectionString, string topicPath) : base(connectionString, topicPath)
		{ }
	}
}
