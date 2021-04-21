using System;
using FluentAssertions;
using Smiosoft.PASS.Publisher;
using Xunit;

namespace Smiosoft.Pass.UnitTests.Publisher
{
	public partial class PublishersServiceTests
	{
		public class Constructor : PublishersServiceTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructing_ThenEnsureNoExceptionsAreThrown()
			{
				Action act = () => new PublishersService(_mockServiceProvider.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructing_ThenEnsureArgumentNullExceptionsAreThrown()
			{
				Action act = () => new PublishersService(null!);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
