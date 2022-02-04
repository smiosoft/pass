using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Publisher
{
	public partial class RabbitMqTopicPublisherTests
	{
		public class Constructor : RabbitMqTopicPublisherTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructinggWithConnectionFactory_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicPublisher(_mockConnectionFactory.Object, "tests", "unit.test");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicPublisher("localhost", "tests", "unit.test");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructinggWithConnectionFactory_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneTopicPublisher(factory: null!, exchangeName: null!, routingKey: null!);

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
				Action act = () => new MessageOneTopicPublisher(hostName, exchangeName, routingKey);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
