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
        public class OnRegistrationAsync : TopicSubscriberTests
        {
            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenNoExceptionsAreThrown()
            {
                Func<Task> act = async () => await _sut.OnRegistrationAsync(CancellationToken.None);

                await act.Should().NotThrowAsync();
            }

            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenStartProcessingOnce()
            {
                await _sut.OnRegistrationAsync(CancellationToken.None);

                _mockServiceBusProcessor.Verify(
                    _ => _.StartProcessingAsync(It.IsAny<CancellationToken>()),
                    Times.Once);
            }
        }
    }
}
