using NSubstitute;
using Smiosoft.PASS.Provider;

namespace Smiosoft.PASS.UnitTests
{
    public partial class PassTests
    {
        private readonly ServiceFactory _mockServiceFactory;
        private readonly Pass _sut;

        public PassTests()
        {
            _mockServiceFactory = Substitute.For<ServiceFactory>();

            _sut = new Pass(_mockServiceFactory);
        }
    }
}
