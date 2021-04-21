using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.Publisher;

namespace Smiosoft.Pass.UnitTests.Publisher
{
	public partial class PublishersServiceTests
	{
		private readonly Mock<IServiceProvider> _mockServiceProvider;
		private readonly PublishersService _sut;

		public PublishersServiceTests()
		{
			_mockServiceProvider = new Mock<IServiceProvider>();

			var serviceScope = new Mock<IServiceScope>();
			serviceScope.Setup(x => x.ServiceProvider).Returns(_mockServiceProvider.Object);

			var serviceScopeFactory = new Mock<IServiceScopeFactory>();
			serviceScopeFactory
				.Setup(x => x.CreateScope())
				.Returns(serviceScope.Object);

			_mockServiceProvider
				.Setup(x => x.GetService(typeof(IServiceScopeFactory)))
				.Returns(serviceScopeFactory.Object);

			_sut = new PublishersService(_mockServiceProvider.Object);
		}
	}
}
