using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
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
            public async Task GivenConfiguredSubscriber_WhenExected_ThenStartProcessingOnce()
            {
                await _sut.RegisterAsync(CancellationToken.None);

                _mockServiceBusProcessor.Verify(
                    _ => _.StartProcessingAsync(It.IsAny<CancellationToken>()),
                    Times.Once);
            }
        }
    }
}
