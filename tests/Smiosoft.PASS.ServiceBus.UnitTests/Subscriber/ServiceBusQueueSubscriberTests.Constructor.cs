using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
	public partial class ServiceBusQueueSubscriberTests
	{
		public class Constructor : ServiceBusQueueSubscriberTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueueSubscriber("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue");

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
				Action act = () => new MessageOneQueueSubscriber(connectionString, queueName);

				act.Should().NotThrow();
			}
		}
	}
}
