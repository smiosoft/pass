using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Publishers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Topic
{
	public partial class TopicPublisherTests
	{
		public class Constructor : TopicPublisherTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithClient_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicPublisher(_mockTopicClient.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicPublisher("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructingWithClient_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneTopicPublisher(null!);

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
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string connectionString, string topicPath)
			{
				Action act = () => new MessageOneTopicPublisher(connectionString, topicPath);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
