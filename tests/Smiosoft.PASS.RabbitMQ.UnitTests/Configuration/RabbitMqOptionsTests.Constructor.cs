using System;
using FluentAssertions;
using Smiosoft.PASS.RabbitMQ.Configuration;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Configuration
{
	public partial class RabbitMqOptionsTests
	{
		public class Constructor : RabbitMqOptionsTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructing_ThenNoExceptionsAreThrown()
			{
				Action act = () => new RabbitMqOptions(_mockServiceCollection.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructing_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new RabbitMqOptions(null!);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
