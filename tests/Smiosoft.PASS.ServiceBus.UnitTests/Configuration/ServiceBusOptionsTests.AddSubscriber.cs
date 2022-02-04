using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers.Mocks.Factories;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Configuration
{
	public partial class ServiceBusOptionsTests
	{
		public class AddSubscriber : ServiceBusOptionsTests
		{
			[Fact]
			public void GivenConfiguredQueueSubscriber_WhenExecuted_ThenSubscriberIsAddedAsSingleton()
			{
				_sut.AddSubscriber<MessageOneQueueSubscriber>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBaseSubscriber), typeof(MessageOneQueueSubscriber))),
					Times.Once);
			}

			[Fact]
			public void GivenConfiguredTopicSubscriber_WhenExecuted_ThenSubscriberIsAddedAsSingleton()
			{
				_sut.AddSubscriber<MessageOneTopicSubscriber>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBaseSubscriber), typeof(MessageOneTopicSubscriber))),
					Times.Once);
			}
		}
	}
}
