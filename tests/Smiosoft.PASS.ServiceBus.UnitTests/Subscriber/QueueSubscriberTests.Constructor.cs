using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        public class Constructor : QueueSubscriberTests
        {
            [Fact]
            public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
            {
                Action act = () => new Subscribers.QueueSubscriberOne(
                    "Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***",
                    "test-queue",
                    _mockServiceBusProcessor.Object);

                act.Should().NotThrow();
            }

            [Theory]
            [InlineData(null, null)]
            [InlineData(null, "")]
            [InlineData(null, " ")]
            [InlineData("", null)]
            [InlineData(" ", null)]
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string connectionString, string queueName)
            {
                Action act = () => new Subscribers.QueueSubscriberOne(connectionString, queueName, processor: null);

                act.Should().NotThrow();
            }
        }
    }
}
