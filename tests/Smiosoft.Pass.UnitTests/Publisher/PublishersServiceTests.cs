using System;
using Moq;
using Smiosoft.Pass.UnitTests.Helpers.Mocks.Factories;
using Smiosoft.PASS.Publisher;

namespace Smiosoft.Pass.UnitTests.Publisher
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
