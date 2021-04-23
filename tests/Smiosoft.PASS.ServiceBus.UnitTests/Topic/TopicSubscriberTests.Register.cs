using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Topic
{
	public partial class TopicSubscriberTests
	{
		public class Register : TopicSubscriberTests
		{
			[Fact]
			public void GivenConfiguredSubscriber_WhenExected_ThenClientRegisteresMessageHandlerOnce()
			{
				_sut.Register();

				_mockSubscriptionClient.Verify(
					_ => _.RegisterMessageHandler(It.IsAny<Func<Message, CancellationToken, Task>>(), It.IsAny<MessageHandlerOptions>()),
					Times.Once);
			}
		}
	}
}
