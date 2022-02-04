using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
	public partial class RabbitMqQueuePublisherTests
	{
		public class PublishAsync : RabbitMqQueuePublisherTests
		{
			[Fact]
			public async Task GivenConfiguredPublisher_WhenExected_ThenQueueIsDeclaredOnce()
			{
				await _sut.PublishAsync(new DummyTestMessageOne());

				_mockChannel.Verify(
					_ => _.QueueDeclare(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
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
