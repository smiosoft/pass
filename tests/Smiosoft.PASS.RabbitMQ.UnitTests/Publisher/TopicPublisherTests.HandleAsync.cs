using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RabbitMQ.Client;
using Smiosoft.PASS.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
    public partial class TopicPublisherTests
    {
        public class HandleAsync : TopicPublisherTests
        {
            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenNoExceptionsAreThrown()
            {
                Func<Task> act = async () => await _sut.HandleAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                await act.Should().NotThrowAsync();
            }

            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenExchangeIsDeclaredOnce()
            {
                await _sut.HandleAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                _mockChannel.Verify(
                    _ => _.ExchangeDeclare(It.IsAny<string>(), It.Is<string>(_ => _ == "topic"), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
                    Times.Once);
            }

            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenBasicPublishOnce()
            {
                await _sut.HandleAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                _mockChannel.Verify(
                    _ => _.BasicPublish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<IBasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>()),
                    Times.Once);
            }
        }
    }
}
