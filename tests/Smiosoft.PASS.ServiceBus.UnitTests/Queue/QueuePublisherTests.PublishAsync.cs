using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Moq;
using Smiosoft.PASS.UnitTests.Helpers.Messages;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Queue
{
	public partial class QueuePublisherTests
	{
		public class PublishAsync : QueuePublisherTests
		{
			[Fact]
			public async Task GivenConfiguredPublisher_WhenExected_ThenMessageIsPublishedOnce()
			{
				await _sut.PublishAsync(new DummyTestMessageOne());

				_mockQueueClient.Verify(_ => _.SendAsync(It.IsAny<Message>()), Times.Once);
			}
		}
	}
}
