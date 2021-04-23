using System;
using Moq;
using Smiosoft.PASS.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers.Mocks.Factories;

namespace Smiosoft.PASS.UnitTests.Subscriber
{
	public partial class HostedSubscribersServiceTests
	{
		private readonly Mock<IServiceProvider> _mockServiceProvider;
		private readonly HostedSubscribersService _sut;

		public HostedSubscribersServiceTests()
		{
			_mockServiceProvider = MockServiceProviderFactory.Create();

			_sut = new HostedSubscribersService(_mockServiceProvider.Object);
		}
	}
}
