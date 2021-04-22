using System;
using Moq;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers.Mocks.Factories;

namespace Smiosoft.PASS.UnitTests.Publisher
{
	public partial class PublishersServiceTests
	{
		private readonly Mock<IServiceProvider> _mockServiceProvider;
		private readonly PublishersService _sut;

		public PublishersServiceTests()
		{
			_mockServiceProvider = MockServiceProviderFactory.Create();

			_sut = new PublishersService(_mockServiceProvider.Object);
		}
	}
}