using NSubstitute;
using Smiosoft.PASS.Provider;
using Smiosoft.PASS.Subscriber.Services;

namespace Smiosoft.PASS.UnitTests.Subscriber.Services
{
	public partial class HostedSubscribersTests
	{
		private readonly ServiceFactory _mockServiceFactory;
		private readonly HostedSubscribers _sut;

		public HostedSubscribersTests()
		{
			_mockServiceFactory = Substitute.For<ServiceFactory>();

			_sut = new HostedSubscribers(_mockServiceFactory);
		}
	}
}
