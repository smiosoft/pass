using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        public class OnExceptionAsync : QueueSubscriberTests
        {
            [Fact]
            public async Task GivenConfiguredSubscriber_WhenExected_ThenNoExceptionsAreThrown()
            {
                Func<Task> act = async () => await _sut.OnExceptionAsync(new Exception());

                await act.Should().NotThrowAsync();
            }
        }
    }
}
