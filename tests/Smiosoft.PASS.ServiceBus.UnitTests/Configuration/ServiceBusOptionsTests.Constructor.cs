using System;
using FluentAssertions;
using Smiosoft.PASS.ServiceBus.Configuration;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Configuration
{
	public partial class ServiceBusOptionsTests
	{
		public class Constructor : ServiceBusOptionsTests
		{
			[Fact]
			public void GivenValidParameters_WhenConstructing_ThenNoExceptionsAreThrown()
			{
				Action act = () => new ConfigureServiceBusOptions(_mockServiceCollection.Object);

				act.Should().NotThrow();
			}

			[Fact]
			public void GivenNullParameters_WhenConstructing_ThenArgumentNullExceptionIsThrown()
			{
				Action act = () => new ConfigureServiceBusOptions(null!);

				act.Should().Throw<ArgumentNullException>();
			}
		}
	}
}
