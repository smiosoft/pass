using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Publishers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Queue
{
	public partial class QueuePublisherTests
	{
		public class Constructor : QueuePublisherTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithClient_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueuePublisher(_mockQueueClient.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueuePublisher("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructingWithClient_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneQueuePublisher(null!);

				act.Should().Throw<ArgumentNullException>();
			}

			[Theory]
			[InlineData(null, null)]
			[InlineData("", "")]
			[InlineData(" ", " ")]
			[InlineData("", null)]
			[InlineData(" ", null)]
			[InlineData(null, "")]
			[InlineData(null, " ")]
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string connectionString, string queueName)
			{
				Action act = () => new MessageOneQueuePublisher(connectionString, queueName);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
