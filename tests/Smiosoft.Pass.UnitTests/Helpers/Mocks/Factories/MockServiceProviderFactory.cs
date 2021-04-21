using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Smiosoft.Pass.UnitTests.Helpers.Mocks.Factories
{
	public static class MockServiceProviderFactory
	{
		public static Mock<IServiceProvider> Create()
		{
			var mockServiceProvider = new Mock<IServiceProvider>();

			var serviceScope = new Mock<IServiceScope>();
			serviceScope.Setup(x => x.ServiceProvider).Returns(mockServiceProvider.Object);

			var serviceScopeFactory = new Mock<IServiceScopeFactory>();
			serviceScopeFactory
				.Setup(x => x.CreateScope())
				.Returns(serviceScope.Object);

			mockServiceProvider
				.Setup(x => x.GetService(typeof(IServiceScopeFactory)))
				.Returns(serviceScopeFactory.Object);

			return mockServiceProvider;
		}
	}
}
