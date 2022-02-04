using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
	public partial class RabbitMqQueuePublisherTests
	{
		public class Constructor : RabbitMqQueuePublisherTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionFactory_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueuePublisher(_mockConnectionFactory.Object, "test-queue");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueuePublisher("localhost", "test-queue");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructingWithConnectionFactory_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneQueuePublisher(factory: null!, queueName: null!);

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
				Action act = () => new MessageOneQueuePublisher(hostName, queueName);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
