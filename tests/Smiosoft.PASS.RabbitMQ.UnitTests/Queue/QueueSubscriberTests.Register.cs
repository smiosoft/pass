using System.Collections.Generic;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Queue
{
	public partial class QueueSubscriberTests
	{
		public class Register : QueueSubscriberTests
		{
			[Fact]
			public void GivenConfiguredSubscriber_WhenExected_ThenQueueIsDeclaredOnce()
			{
				_sut.Register();

				_mockChannel.Verify(
					_ => _.QueueDeclare(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
					Times.Once);
			}

			[Fact]
			public void GivenConfiguredSubscriber_WhenExected_ThenBasicConsumerIsDeclaredOnce()
			{
				_sut.Register();

				_mockChannel.Verify(
					_ => _.BasicConsume(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>(), It.IsAny<IBasicConsumer>()),
					Times.Once);
			}
		}
	}
}
