using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
    public partial class QueuePublisherTests
    {
        public class Constructor : QueuePublisherTests
        {
            [Fact]
            public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
            {
                Action act = () => new Publishers.QueuePublisherOne("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue");

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
            public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string connectionString, string queueName)
            {
                Action act = () => new Publishers.QueuePublisherOne(connectionString, queueName);

                act.Should().NotThrow();
            }
        }
    }
}
