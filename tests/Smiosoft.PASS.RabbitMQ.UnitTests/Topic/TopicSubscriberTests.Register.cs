using System.Collections.Generic;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Topic
{
	public partial class TopicSubscriberTests
	{
		public class Register : TopicSubscriberTests
		{
			[Fact]
			public void GivenConfiguredSubscriber_WhenExected_ThenExchangeIsDeclaredOnce()
			{
				_sut.Register();

				_mockChannel.Verify(
					_ => _.ExchangeDeclare(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
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
