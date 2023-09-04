using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RabbitMQ.Client;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class TopicSubscriberTests
    {
        public class RegisterAsync : TopicSubscriberTests
        {
            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenNoExceptionsAreThrown()
            {
                Func<Task> act = async () => await _sut.RegisterAsync(CancellationToken.None);

                await act.Should().NotThrowAsync();
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenExchangeIsDeclaredOnce()
            {
                await _sut.RegisterAsync(CancellationToken.None);

                _mockChannel.Received(1)
                    .ExchangeDeclare(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<IDictionary<string, object>>());
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenQueueIsDeclaredOnce()
            {
                await _sut.RegisterAsync(CancellationToken.None);

                _mockChannel.Received(1)
                    .QueueDeclare(Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<IDictionary<string, object>>());
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenQueueIsBoundOnce()
            {
                await _sut.RegisterAsync(CancellationToken.None);

                _mockChannel.Received(1)
                    .QueueBind(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IDictionary<string, object>>());
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenBasicConsumerIsDeclaredOnce()
            {
                await _sut.RegisterAsync(CancellationToken.None);

                _mockChannel.Received(1)
                    .BasicConsume(Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<IDictionary<string, object>>(), Arg.Any<IBasicConsumer>());
            }
        }
    }
}
