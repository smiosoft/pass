using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Mocks.Factories;
using Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers.Publishers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Configuration
{
	public partial class ServiceBusOptionsTests
	{
		public class AddTopicPublisher : ServiceBusOptionsTests
		{
			[Fact]
			public void GivenConfiguredPublisher_WhenExecuted_ThenPublisherIsAddedAsSingleton()
			{
				_sut.AddTopicPublisher<MessageOneTopicPublisher>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBasePublisher), typeof(MessageOneTopicPublisher))),
					Times.Once);
			}
		}
	}
}
