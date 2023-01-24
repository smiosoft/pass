using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
    public partial class TopicSubscriberTests
    {
        public class Constructor : TopicSubscriberTests
        {
            [Theory]
            [InlineData(null, null, null, null)]
            [InlineData(null, null, null, "")]
            [InlineData(null, null, null, " ")]
            [InlineData(null, null, "", null)]
            [InlineData(null, null, " ", null)]
            [InlineData(null, "", null, null)]
            [InlineData(null, " ", null, null)]
            [InlineData("", null, null, null)]
            [InlineData(" ", null, null, null)]
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string hostName, string exchangeName, string queueName, string routingKey)
            {
                Action act = () => new Subscribers.TopicSubscriberOne(hostName, exchangeName, queueName, routingKey, factory: _mockConnectionFactory.Object);

                act.Should().NotThrow();
            }
        }
    }
}
