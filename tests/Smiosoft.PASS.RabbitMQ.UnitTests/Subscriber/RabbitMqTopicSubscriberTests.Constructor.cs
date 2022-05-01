using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Subscriber
{
	public partial class RabbitMqTopicSubscriberTests
	{
		public class Constructor : RabbitMqTopicSubscriberTests
		{
			[Theory]
			[InlineData(null, null, null)]
			[InlineData(null, null, "")]
			[InlineData(null, null, " ")]
			[InlineData(null, "", null)]
			[InlineData(null, "", "")]
			[InlineData(null, "", " ")]
			[InlineData(null, " ", null)]
			[InlineData(null, " ", "")]
			[InlineData(null, " ", " ")]
			[InlineData("", null, null)]
			[InlineData("", null, "")]
			[InlineData("", null, " ")]
			[InlineData("", "", null)]
			[InlineData("", "", "")]
			[InlineData("", "", " ")]
			[InlineData("", " ", null)]
			[InlineData("", " ", "")]
			[InlineData("", " ", " ")]
			[InlineData(" ", null, null)]
			[InlineData(" ", null, "")]
			[InlineData(" ", null, " ")]
			[InlineData(" ", "", null)]
			[InlineData(" ", "", "")]
			[InlineData(" ", "", " ")]
			[InlineData(" ", " ", null)]
			[InlineData(" ", " ", "")]
			[InlineData(" ", " ", " ")]
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown(string hostName, string exchangeName, string routingKey)
			{
				Action act = () => new MessageOneTopicSubscriber(hostName, exchangeName, routingKey);

				act.Should().NotThrow();
			}
		}
	}
}
