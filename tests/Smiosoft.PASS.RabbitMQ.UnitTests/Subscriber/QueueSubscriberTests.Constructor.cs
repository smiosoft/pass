using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class QueueSubscriberTests
    {
        public class Constructor : QueueSubscriberTests
        {
            [Theory]
            [InlineData(null, null)]
            [InlineData(null, "")]
            [InlineData(null, " ")]
            [InlineData("", null)]
            [InlineData(" ", null)]
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string hostName, string queueName)
            {
                Action act = () => new Subscribers.QueueSubscriberOne(hostName, queueName, factory: _mockConnectionFactory);

                act.Should().NotThrow();
            }
        }
    }
}
