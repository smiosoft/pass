using Moq;
using Smiosoft.PASS.Provider;
using Smiosoft.PASS.Subscriber.Services;

namespace Smiosoft.PASS.UnitTests.Subscriber.Services
{
	public partial class HostedSubscribersTests
	{
		private readonly Mock<ServiceFactory> _mockServiceFactory;
		private HostedSubscribers _sut;

		public HostedSubscribersTests()
		{
			_mockServiceFactory = new Mock<ServiceFactory>();

			_sut = new HostedSubscribers(_mockServiceFactory.Object);
		}
	}
}
