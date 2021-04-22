using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Queue
{
	public partial class QueueSubscriberTests
	{
		public class Constructor : QueueSubscriberTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithClient_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueueSubscriber(_mockQueueClient.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueueSubscriber("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructingWithClient_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneQueueSubscriber(null!);

				act.Should().Throw<ArgumentNullException>();
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
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string connectionString, string queueName)
			{
				Action act = () => new MessageOneQueueSubscriber(connectionString, queueName);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
