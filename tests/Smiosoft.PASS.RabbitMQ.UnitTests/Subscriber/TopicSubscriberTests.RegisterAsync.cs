using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
	public partial class TopicSubscriberTests
	{
		public class RegisterAsync : TopicSubscriberTests
		{
			[Fact]
			public async Task GivenConfiguredSubscriber_WhenExected_ThenExchangeIsDeclaredOnce()
			{
				await _sut.RegisterAsync();

				_mockChannel.Verify(
					_ => _.ExchangeDeclare(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
					Times.Once);
			}

			[Fact]
			public async Task GivenConfiguredSubscriber_WhenExected_ThenBasicConsumerIsDeclaredOnce()
			{
				await _sut.RegisterAsync();

				_mockChannel.Verify(
					_ => _.BasicConsume(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>(), It.IsAny<IBasicConsumer>()),
					Times.Once);
			}
		}
	}
}
