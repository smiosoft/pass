using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Topic
{
	public partial class TopicPublisherTests
	{
		public class PublishAsync : TopicPublisherTests
		{
			[Fact]
			public async Task GivenConfiguredPublisher_WhenExected_ThenExchangeIsDeclaredOnce()
			{
				await _sut.PublishAsync(new DummyTestMessageOne());

				_mockChannel.Verify(
					_ => _.ExchangeDeclare(It.IsAny<string>(), It.Is<string>(_ => _ == "topic"), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
					Times.Once);
			}

			[Fact]
			public async Task GivenConfiguredPublisher_WhenExected_ThenBasicPublishOnce()
			{
				await _sut.PublishAsync(new DummyTestMessageOne());

				_mockChannel.Verify(
					_ => _.BasicPublish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<IBasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>()),
					Times.Once);
			}
		}
	}
}
