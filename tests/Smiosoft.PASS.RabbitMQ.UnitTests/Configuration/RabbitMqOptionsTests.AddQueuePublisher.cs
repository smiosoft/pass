using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers;
using Smiosoft.PASS.UnitTests.TestHelpers.Mocks.Factories;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Configuration
{
	public partial class RabbitMqOptionsTests
	{
		public class AddQueuePublisher : RabbitMqOptionsTests
		{
			[Fact]
			public void GivenConfiguredPublisher_WhenExecuted_ThenPublisherIsAddedAsSingleton()
			{
				_sut.AddQueuePublisher<MessageOneQueuePublisher>();

				_mockServiceCollection.Verify(
					_ => _.Add(MockServiceDescriptorFactory.CreateIt(ServiceLifetime.Singleton, typeof(IBasePublisher), typeof(MessageOneQueuePublisher))),
					Times.Once);
			}
		}
	}
}