using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.Helpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Topic
{
	public partial class TopicSubscriberTests
	{
		public class Constructor : TopicSubscriberTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithClient_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicSubscriber(_mockSubscriptionClient.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicSubscriber("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic", "test-subscription");

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructingWithClient_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new MessageOneTopicSubscriber(null!);

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
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string connectionString, string topicPath, string subscriptionName)
			{
				Action act = () => new MessageOneTopicSubscriber(connectionString, topicPath, subscriptionName);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
