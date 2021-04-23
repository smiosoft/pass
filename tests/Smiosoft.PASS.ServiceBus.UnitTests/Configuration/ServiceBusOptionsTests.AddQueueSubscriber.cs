using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Mocks.Factories;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.Subscriber;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Configuration
{
	public partial class ServiceBusOptionsTests
	{
		public class AddQueueSubscriber : ServiceBusOptionsTests
		{
			[Fact]
			public void GivenConfiguredSubscriber_WhenExecuted_ThenSubscriberIsAddedAsSingleton()
			{
				_sut.AddQueueSubscriber<MessageOneQueueSubscriber>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBaseSubscriber), typeof(MessageOneQueueSubscriber))),
					Times.Once);
			}
		}
	}
}
