using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        public class OnRegistrationAsync : QueueSubscriberTests
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

                await _mockServiceBusProcessor.Received(1).StartProcessingAsync(Arg.Any<CancellationToken>());
            }
        }
    }
}
