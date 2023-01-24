using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Smiosoft.PASS.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        public class OnReceivedAsync : QueueSubscriberTests
        {
            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenNoExceptionsAreThrown()
            {
                Func<Task> act = async () => await _sut.OnReceivedAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                await act.Should().NotThrowAsync();
            }
        }
    }
}
