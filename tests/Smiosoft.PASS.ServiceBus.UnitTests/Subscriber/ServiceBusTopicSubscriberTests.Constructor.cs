using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
	public partial class ServiceBusTopicSubscriberTests
	{
		public class Constructor : ServiceBusTopicSubscriberTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructingWithConnectionParams_ThenNoExceptionsAreThrown()
			{
				Action act = () => new MessageOneTopicSubscriber("Endpoint=sb://test.net/;SharedAccessKeyName=***;SharedAccessKey=***", "test-topic", "test-subscription");

				act.Should().NotThrow();
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
			public void GivenInvalidParameters_WhenConstructingWithConnectionParams_ThenArgumentNullExceptionIsThrown(string connectionString, string topicName, string subscriptionName)
			{
				Action act = () => new MessageOneTopicSubscriber(connectionString, topicName, subscriptionName);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
