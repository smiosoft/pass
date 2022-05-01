using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Publishers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
	public partial class ServiceBusQueuePublisherTests
	{
		public class Constructor : ServiceBusQueuePublisherTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneQueuePublisher("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-queue");

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
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string connectionString, string queueName)
			{
				Action act = () => new MessageOneQueuePublisher(connectionString, queueName);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}