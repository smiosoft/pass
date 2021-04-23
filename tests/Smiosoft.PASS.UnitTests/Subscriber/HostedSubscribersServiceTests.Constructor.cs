using System;
using FluentAssertions;
using Smiosoft.PASS.Subscriber;
using Xunit;

namespace Smiosoft.PASS.UnitTests.Subscriber
{
	public partial class HostedSubscribersServiceTests
	{
		public class Constructor : HostedSubscribersServiceTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructing_ThenNoExceptionsAreThrown()
			{
				Action act = () => new HostedSubscribersService(_mockServiceProvider.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructing_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new HostedSubscribersService(null!);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
