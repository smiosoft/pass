using System;
using FluentAssertions;
using Smiosoft.PASS.Publisher;
using Xunit;

namespace Smiosoft.PASS.UnitTests.Publisher
{
	public partial class PublishersServiceTests
	{
		public class Constructor : PublishersServiceTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructing_ThenNoExceptionsAreThrown()
			{
				Action act = () => new PublishingService(_mockServiceProvider.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructing_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new PublishingService(null!);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
