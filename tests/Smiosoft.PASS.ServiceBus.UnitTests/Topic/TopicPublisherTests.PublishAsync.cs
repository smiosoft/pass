using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Topic
{
	public partial class TopicPublisherTests
	{
		public class PublishAsync : TopicPublisherTests
		{
			[Fact]
			public async Task GivenConfiguredPublisher_WhenExected_ThenMessageIsPublishedOnce()
			{
				await _sut.PublishAsync(new PASS.UnitTests.TestHelpers.Messages.DummyTestMessageOne());

				_mockTopicClient.Verify(_ => _.SendAsync(It.IsAny<Message>()), Times.Once);
			}
		}
	}
}
