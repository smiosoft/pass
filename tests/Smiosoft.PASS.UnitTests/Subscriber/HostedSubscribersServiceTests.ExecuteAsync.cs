using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Smiosoft.PASS.UnitTests.Helpers.Messages;
using Smiosoft.PASS.Subscriber;
using Xunit;

namespace Smiosoft.PASS.UnitTests.Subscriber
{
	public partial class HostedSubscribersServiceTests
	{
		public class ExecuteAsync : HostedSubscribersServiceTests
		{
			private readonly Mock<ISubscriber<object, DummyTestMessageOne>> _mockMessageOneSubscriber;
			private readonly Mock<ISubscriber<object, DummyTestMessageTwo>> _mockMessageTwoSubscriber;
			private readonly Mock<ISubscriber<object, DummyTestMessageThree>> _mockMessageThreeSubscriber;

			public ExecuteAsync()
			{
				_mockMessageOneSubscriber = new Mock<ISubscriber<object, DummyTestMessageOne>>();
				_mockMessageTwoSubscriber = new Mock<ISubscriber<object, DummyTestMessageTwo>>();
				_mockMessageThreeSubscriber = new Mock<ISubscriber<object, DummyTestMessageThree>>();
			}

			[Fact]
			public async Task GivenMultipleConfiguredSubscribers_WhenExected_ThenRegisterAllSubscribers()
			{
				_mockServiceProvider
					.Setup(_ => _.GetService(typeof(IEnumerable<IBaseSubscriber>)))
					.Returns(new IBaseSubscriber[]
					{
						_mockMessageOneSubscriber.Object,
						_mockMessageTwoSubscriber.Object,
						_mockMessageThreeSubscriber.Object
					});

				await _sut.StartAsync(CancellationToken.None);

				_mockMessageOneSubscriber.Verify(_ => _.Register(), Times.Once);
				_mockMessageTwoSubscriber.Verify(_ => _.Register(), Times.Once);
				_mockMessageThreeSubscriber.Verify(_ => _.Register(), Times.Once);
			}
		}
	}
}
