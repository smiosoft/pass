using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Queue
{
	public partial class QueueSubscriberTests
	{
		public class Register : QueueSubscriberTests
		{
			[Fact]
			public void GivenConfiguredSubscriber_WhenExected_ThenClientRegisteresMessageHandlerOnce()
			{
				_sut.Register();

				_mockQueueClient.Verify(
					_ => _.RegisterMessageHandler(It.IsAny<Func<Message, CancellationToken, Task>>(), It.IsAny<MessageHandlerOptions>()),
					Times.Once);
			}
		}
	}
}
