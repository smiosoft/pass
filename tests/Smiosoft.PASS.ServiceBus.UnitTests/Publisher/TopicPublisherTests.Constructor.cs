using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
    public partial class TopicPublisherTests
    {
        public class Constructor : TopicPublisherTests
        {
            [Fact]
            public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
            {
                Action act = () => new Publishers.TopicPublisherOne("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic");

                act.Should().NotThrow();
            }

            [Theory]
            [InlineData(null, null)]
            [InlineData(null, "")]
            [InlineData(null, " ")]
            [InlineData("", null)]
            [InlineData(" ", null)]
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string connectionString, string topicName)
            {
                Action act = () => new Publishers.TopicPublisherOne(connectionString, topicName);

                act.Should().NotThrow();
            }
        }
    }
}
