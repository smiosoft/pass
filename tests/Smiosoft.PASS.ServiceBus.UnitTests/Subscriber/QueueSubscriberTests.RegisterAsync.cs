using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Subscriber
{
	public partial class QueueSubscriberTests
	{
		public class RegisterAsync : QueueSubscriberTests
		{
			[Fact]
			public async Task GivenConfiguredQueueSubscriber_WhenExected_ThenStartProcessingOnce()
			{
				await _sut.RegisterAsync();

				_mockServiceBusProcessor.Verify(
					_ => _.StartProcessingAsync(It.IsAny<CancellationToken>()),
					Times.Once);
			}
		}
	}
}
