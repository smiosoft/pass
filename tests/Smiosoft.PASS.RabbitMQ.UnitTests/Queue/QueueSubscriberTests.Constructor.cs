using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Queue
{
	public partial class QueueSubscriberTests
	{
		public class Constructor : QueueSubscriberTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionFactory_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueueSubscriber(_mockConnectionFactory.Object, "test-queue");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructingWithConnectionFactory_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneQueueSubscriber(factory: null!, queueName: null!);

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
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string hostName, string queueName)
			{
				Action act = () => new MessageOneQueueSubscriber(hostName, queueName);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
