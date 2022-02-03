using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Subscribers;
using Smiosoft.PASS.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers.Mocks.Factories;
using Xunit;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Configuration
{
	public partial class RabbitMqOptionsTests
	{
		public class AddQueueSubscriber : RabbitMqOptionsTests
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
