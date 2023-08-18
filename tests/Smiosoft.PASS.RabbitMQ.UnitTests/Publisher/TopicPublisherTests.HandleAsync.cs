using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
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

                _mockChannel.Received(1)
                    .ExchangeDeclare(Arg.Any<string>(), Arg.Is<string>(_ => _ == "topic"), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<IDictionary<string, object>>());
            }

            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenBasicPublishOnce()
            {
                await _sut.HandleAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                _mockChannel.Received(1)
                    .BasicPublish(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<IBasicProperties>(), Arg.Any<ReadOnlyMemory<byte>>());
            }
        }
    }
}
