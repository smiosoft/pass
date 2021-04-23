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
		public class AddTopicSubscriber : ServiceBusOptionsTests
		{
			[Fact]
			public void GivenConfiguredSubscriber_WhenExecuted_ThenSubscriberIsAddedAsSingleton()
			{
				_sut.AddTopicSubscriber<MessageOneTopicSubscriber>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBaseSubscriber), typeof(MessageOneTopicSubscriber))),
					Times.Once);
			}
		}
	}
}
