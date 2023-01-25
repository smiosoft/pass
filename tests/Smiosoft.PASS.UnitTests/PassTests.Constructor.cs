using System;
using FluentAssertions;
using Xunit;

namespace Smiosoft.PASS.UnitTests
{
    public partial class PassTests
    {
        public class Constructor : PassTests
        {
            [Fact]
            public void GivenValidParameters_WhenConstructing_ThenNoExceptionsAreThrown()
            {
                Action act = () => new Pass(_mockServiceFactory.Object);

                act.Should().NotThrow();
            }

            [Fact]
            public void GivenNullParameters_WhenConstructing_ThenArgumentNullExceptionIsThrown()
            {
                Action act = () => new Pass(null!);

                act.Should().Throw<ArgumentNullException>();
            }
        }
    }
}
