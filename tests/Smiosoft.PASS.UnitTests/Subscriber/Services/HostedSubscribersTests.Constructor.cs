using System;
using FluentAssertions;
using Smiosoft.PASS.Subscriber.Services;
using Xunit;

namespace Smiosoft.PASS.UnitTests.Subscriber.Services
{
	public partial class HostedSubscribersTests
	{
		public class Constructor : HostedSubscribersTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructing_ThenNoExceptionsAreThrown()
			{
				Action act = () => new HostedSubscribers(_mockServiceFactory.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructing_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new HostedSubscribers(null!);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
