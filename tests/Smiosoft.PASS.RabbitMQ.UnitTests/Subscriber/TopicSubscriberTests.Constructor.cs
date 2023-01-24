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
            [InlineData(null, null, null)]
            [InlineData(null, null, "")]
            [InlineData(null, null, " ")]
            [InlineData(null, "", null)]
            [InlineData(null, "", "")]
            [InlineData(null, "", " ")]
            [InlineData(null, " ", null)]
            [InlineData(null, " ", "")]
            [InlineData(null, " ", " ")]
            [InlineData("", null, null)]
            [InlineData("", null, "")]
            [InlineData("", null, " ")]
            [InlineData("", "", null)]
            [InlineData("", "", "")]
            [InlineData("", "", " ")]
            [InlineData("", " ", null)]
            [InlineData("", " ", "")]
            [InlineData("", " ", " ")]
            [InlineData(" ", null, null)]
            [InlineData(" ", null, "")]
            [InlineData(" ", null, " ")]
            [InlineData(" ", "", null)]
            [InlineData(" ", "", "")]
            [InlineData(" ", "", " ")]
            [InlineData(" ", " ", null)]
            [InlineData(" ", " ", "")]
            [InlineData(" ", " ", " ")]
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string hostName, string exchangeName, string routingKey)
            {
                Action act = () => new Subscribers.TopicSubscriberOne(hostName, exchangeName, routingKey, factory: null);

                act.Should().NotThrow();
            }
        }
    }
}
