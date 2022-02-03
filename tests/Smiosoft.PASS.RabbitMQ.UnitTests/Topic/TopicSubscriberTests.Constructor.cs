using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Topic
{
	public partial class TopicSubscriberTests
	{
		public class Constructor : TopicSubscriberTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionFactory_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicSubscriber(_mockConnectionFactory.Object, "tests", "unit.test");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructingWithConnectionFactory_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneTopicSubscriber(factory: null!, exchangeName: null!, routingKey: null!);

				act.Should().Throw<ArgumentNullException>();
			}

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
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string hostName, string exchangeName, string routingKey)
			{
				Action act = () => new MessageOneTopicSubscriber(hostName, exchangeName, routingKey);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
