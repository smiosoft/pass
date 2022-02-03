using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Smiosoft.PASS.UnitTests.TestHelpers.Mocks.Factories
{
	public static class MockServiceDescriptorFactory
	{
		public static ServiceDescriptor CreateIt(ServiceLifetime lifetime, Type service, Type implementation)
		{
			return It.Is<ServiceDescriptor>(_ =>
				_.Lifetime == lifetime
				&& _.ServiceType == service
				&& _.ImplementationType == implementation);
		}
	}
}
