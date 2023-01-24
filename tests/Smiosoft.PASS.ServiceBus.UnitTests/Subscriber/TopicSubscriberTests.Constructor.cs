using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
    public partial class TopicSubscriberTests
    {
        public class Constructor : TopicSubscriberTests
        {
            [Fact]
            public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
            {
                Action act = () => new Subscribers.TopicSubscriberOne(
                    "Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***",
                    "test-topic",
                    "test-subscription",
                    _mockServiceBusProcessor.Object);

                act.Should().NotThrow();
            }

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
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string connectionString, string topicName, string subscriptionName)
            {
                Action act = () => new Subscribers.TopicSubscriberOne(connectionString, topicName, subscriptionName, processor: null);

                act.Should().NotThrow();
            }
        }
    }
}
