using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
	public partial class RabbitMqQueueSubscriberTests
	{
		public class Constructor : RabbitMqQueueSubscriberTests
		{
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
				Action act = () => new MessageOneQueueSubscriber(hostName, queueName);

				act.Should().NotThrow();
			}
		}
	}
}
