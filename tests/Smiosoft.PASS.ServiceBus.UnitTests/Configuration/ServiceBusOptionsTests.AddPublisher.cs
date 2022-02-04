using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Publishers;
using Smiosoft.PASS.UnitTests.TestHelpers.Mocks.Factories;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Configuration
{
	public partial class ServiceBusOptionsTests
	{
		public class AddPublisher : ServiceBusOptionsTests
		{
			[Fact]
			public void GivenConfiguredQueuePublisher_WhenExecuted_ThenPublisherIsAddedAsSingleton()
			{
				_sut.AddPublisher<MessageOneQueuePublisher>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBasePublisher), typeof(MessageOneQueuePublisher))),
					Times.Once);
			}

			[Fact]
			public void GivenConfiguredTopicPublisher_WhenExecuted_ThenPublisherIsAddedAsSingleton()
			{
				_sut.AddPublisher<MessageOneTopicPublisher>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBasePublisher), typeof(MessageOneTopicPublisher))),
					Times.Once);
			}
		}
	}
}
