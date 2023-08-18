using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
    public partial class TopicPublisherTests
    {
        public class Constructor : TopicPublisherTests
        {
            [Fact]
            public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
            {
                Action act = () => new Publishers.TopicPublisherOne("localhost", "tests", "unit.test", _mockConnectionFactory);

                act.Should().NotThrow();
            }

            [Theory]
            [InlineData(null, null, null)]
            [InlineData(null, null, "")]
            [InlineData(null, null, " ")]
            [InlineData(null, "", null)]
            [InlineData(null, " ", null)]
            [InlineData("", null, null)]
            [InlineData(" ", null, null)]
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string hostName, string exchangeName, string routingKey)
            {
                Action act = () => new Publishers.TopicPublisherOne(hostName, exchangeName, routingKey, factory: _mockConnectionFactory);

                act.Should().NotThrow();
            }
        }
    }
}
