using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Smiosoft.PASS.Publisher;
using Xunit;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.UnitTests.Publisher
{
	public partial class PublishersServiceTests
	{
		public class TryPublishAsync : PublishersServiceTests
		{
			private readonly Mock<IPublisher<object, DummyTestMessageOne>> _mockMessageOnePublisher;
			private readonly Mock<IPublisher<object, DummyTestMessageTwo>> _mockMessageTwoPublisher;
			private readonly Mock<IPublisher<object, DummyTestMessageThree>> _mockMessageThreePublisher;

			public TryPublishAsync()
			{
				_mockMessageOnePublisher = new Mock<IPublisher<object, DummyTestMessageOne>>();
				_mockMessageTwoPublisher = new Mock<IPublisher<object, DummyTestMessageTwo>>();
				_mockMessageThreePublisher = new Mock<IPublisher<object, DummyTestMessageThree>>();
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenExecutedWithARegisteredPublisherMessage_ThenMessageIsPublishedWithASuccessfulResult()
			{
				_mockServiceProvider
					.Setup(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)))
					.Returns(new IBasePublisher[]
					{
						_mockMessageOnePublisher.Object,
						_mockMessageTwoPublisher.Object,
						_mockMessageThreePublisher.Object
					});

				var result = await _sut.TryPublishAsync(new DummyTestMessageOne());

				result.Should().BeTrue();
				_mockMessageOnePublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageOne>()), Times.Once);
				_mockMessageTwoPublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageTwo>()), Times.Never);
				_mockMessageThreePublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageThree>()), Times.Never);
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenExecutedWithAnUnregisteredPublisherMessage_ThenMessageIsNotPublishedWithAFailedResult()
			{
				_mockServiceProvider
					.Setup(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)))
					.Returns(new IBasePublisher[]
					{
						_mockMessageOnePublisher.Object,
						_mockMessageTwoPublisher.Object,
					});

				var result = await _sut.TryPublishAsync(new DummyTestMessageThree());

				result.Should().BeFalse();
				_mockMessageOnePublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageOne>()), Times.Never);
				_mockMessageTwoPublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageTwo>()), Times.Never);
				_mockMessageThreePublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageThree>()), Times.Never);
			}
		}
	}
}
