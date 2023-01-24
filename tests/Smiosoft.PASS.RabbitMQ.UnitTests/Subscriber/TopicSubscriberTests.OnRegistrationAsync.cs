using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class TopicSubscriberTests
    {
        public class OnRegistrationAsync : TopicSubscriberTests
        {
            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenNoExceptionsAreThrown()
            {
                Func<Task> act = async () => await _sut.OnRegistrationAsync(CancellationToken.None);

                await act.Should().NotThrowAsync();
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenExchangeIsDeclaredOnce()
            {
                await _sut.OnRegistrationAsync(CancellationToken.None);

                _mockChannel.Verify(
                    _ => _.ExchangeDeclare(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
                    Times.Once);
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenQueueIsDeclaredOnce()
            {
                await _sut.RegisterAsync(CancellationToken.None);

                _mockChannel.Verify(
                    _ => _.QueueDeclare(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()),
                    Times.Once);
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenQueueIsBoundOnce()
            {
                await _sut.RegisterAsync(CancellationToken.None);

                _mockChannel.Verify(
                    _ => _.QueueBind(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()),
                    Times.Once);
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenBasicConsumerIsDeclaredOnce()
            {
                await _sut.OnRegistrationAsync(CancellationToken.None);

                _mockChannel.Verify(
                    _ => _.BasicConsume(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>(), It.IsAny<IBasicConsumer>()),
                    Times.Once);
            }
        }
    }
}
