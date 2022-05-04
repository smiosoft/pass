using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Smiosoft.PASS.Subscriber;
using Xunit;

namespace Smiosoft.PASS.UnitTests.Subscriber.Services
{
	public partial class HostedSubscribersTests
	{
		public class ExecuteAsync : HostedSubscribersTests
		{
			private readonly Mock<IListener> _mockMessageOneSubscriber;
			private readonly Mock<IListener> _mockMessageTwoSubscriber;
			private readonly Mock<IListener> _mockMessageThreeSubscriber;

			public ExecuteAsync()
			{
				_mockMessageOneSubscriber = new Mock<IListener>();
				_mockMessageTwoSubscriber = new Mock<IListener>();
				_mockMessageThreeSubscriber = new Mock<IListener>();
			}

			[Fact]
			public async Task GivenMultipleConfiguredSubscribers_WhenExected_ThenRegisterAllSubscribers()
			{
				_mockServiceFactory
					.Setup(_ => _.Invoke(typeof(IEnumerable<IListener>)))
					.Returns(new IListener[]
					{
						_mockMessageOneSubscriber.Object,
						_mockMessageTwoSubscriber.Object,
						_mockMessageThreeSubscriber.Object
					});

				await _sut.StartAsync(CancellationToken.None);

				_mockMessageOneSubscriber.Verify(_ => _.RegisterAsync(), Times.Once);
				_mockMessageTwoSubscriber.Verify(_ => _.RegisterAsync(), Times.Once);
				_mockMessageThreeSubscriber.Verify(_ => _.RegisterAsync(), Times.Once);
			}
		}
	}
}
