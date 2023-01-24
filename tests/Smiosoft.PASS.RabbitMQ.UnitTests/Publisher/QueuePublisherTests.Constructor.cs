using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
    public partial class QueuePublisherTests
    {
        public class Constructor : QueuePublisherTests
        {
            [Fact]
            public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
            {
                Action act = () => new Publishers.QueuePublisherOne("localhost", "test-queue", _mockConnectionFactory.Object);

                act.Should().NotThrow();
            }

            [Theory]
            [InlineData(null, null)]
            [InlineData(null, "")]
            [InlineData(null, " ")]
            [InlineData("", null)]
            [InlineData("", "")]
            [InlineData("", " ")]
            [InlineData(" ", null)]
            [InlineData(" ", "")]
            [InlineData(" ", " ")]
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string hostName, string queueName)
            {
                Action act = () => new Publishers.QueuePublisherOne(hostName, queueName, factory: _mockConnectionFactory.Object);

                act.Should().NotThrow();
            }
        }
    }
}
