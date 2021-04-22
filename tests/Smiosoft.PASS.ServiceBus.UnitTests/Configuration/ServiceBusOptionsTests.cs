using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Configuration
{
	public partial class ServiceBusOptionsTests
	{
		private readonly Mock<IServiceCollection> _mockServiceCollection;
		private readonly ServiceBusOptions _sut;

		public ServiceBusOptionsTests()
		{
			_mockServiceCollection = new Mock<IServiceCollection>();

			_sut = new ServiceBusOptions(_mockServiceCollection.Object);
		}
	}
}
