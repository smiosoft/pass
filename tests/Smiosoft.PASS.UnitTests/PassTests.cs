using Moq;
using Smiosoft.PASS.Provider;

namespace Smiosoft.PASS.UnitTests
{
	public partial class PassTests
	{
		private readonly Mock<ServiceFactory> _mockServiceFactory;
		private readonly Pass _sut;

		public PassTests()
		{
			_mockServiceFactory = new Mock<ServiceFactory>();

			_sut = new Pass(_mockServiceFactory.Object);
		}
	}
}
