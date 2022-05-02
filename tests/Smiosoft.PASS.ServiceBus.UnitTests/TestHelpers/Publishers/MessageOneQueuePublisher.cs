using System;
using System.Threading.Tasks;
using Smiosoft.PASS.ServiceBus.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Publishers
{
	public class MessageOneQueuePublisher : ServiceBusQueuePublisher<DummyTestMessageOne>
	{
		public MessageOneQueuePublisher(string connectionString, string queueName) : base(connectionString, queueName)
		{ }

		public override Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}
	}
}
