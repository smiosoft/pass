using System;
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
		public class PublishAsync : PublishersServiceTests
		{
			private readonly Mock<IPublisher<object, DummyTestMessageOne>> _mockMessageOnePublisher;
			private readonly Mock<IPublisher<object, DummyTestMessageTwo>> _mockMessageTwoPublisher;
			private readonly Mock<IPublisher<object, DummyTestMessageThree>> _mockMessageThreePublisher;

			public PublishAsync()
			{
				_mockMessageOnePublisher = new Mock<IPublisher<object, DummyTestMessageOne>>();
				_mockMessageTwoPublisher = new Mock<IPublisher<object, DummyTestMessageTwo>>();
				_mockMessageThreePublisher = new Mock<IPublisher<object, DummyTestMessageThree>>();
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenExecutedWithARegisteredPublisherMessage_ThenMessageIsPublishedWithTheCorrespondingPublisher()
			{
				_mockServiceProvider
					.Setup(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)))
					.Returns(new IBasePublisher[]
					{
						_mockMessageOnePublisher.Object,
						_mockMessageTwoPublisher.Object,
						_mockMessageThreePublisher.Object
					});

				await _sut.PublishAsync(new DummyTestMessageTwo());

				_mockMessageOnePublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageOne>()), Times.Never);
				_mockMessageTwoPublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageTwo>()), Times.Once);
				_mockMessageThreePublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageThree>()), Times.Never);
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenExecutedWithARegisteredPublisherMessageOnce_ThenFetchPublishersFromServiceProvider()
			{
				_mockServiceProvider
					.Setup(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)))
					.Returns(new IBasePublisher[]
					{
						_mockMessageOnePublisher.Object,
						_mockMessageTwoPublisher.Object,
						_mockMessageThreePublisher.Object
					});

				await _sut.PublishAsync(new DummyTestMessageTwo());

				_mockServiceProvider.Verify(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)), Times.Once);
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenExecutedWithARegisteredPublisherMessageMultipleTimes_ThenFetchPublisherFromInternalCache()
			{
				_mockServiceProvider
					.Setup(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)))
					.Returns(new IBasePublisher[]
					{
						_mockMessageOnePublisher.Object,
						_mockMessageTwoPublisher.Object,
						_mockMessageThreePublisher.Object
					});

				await _sut.PublishAsync(new DummyTestMessageTwo());
				await _sut.PublishAsync(new DummyTestMessageTwo());
				await _sut.PublishAsync(new DummyTestMessageTwo());
				await _sut.PublishAsync(new DummyTestMessageTwo());

				_mockServiceProvider.Verify(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)), Times.Once);
				_mockMessageTwoPublisher.Verify(_ => _.PublishAsync(It.IsAny<DummyTestMessageTwo>()), Times.Exactly(4));
			}

			[Fact]
			public void GiventMultipleConfiguredPublishers_WhenExecutedWithAnUnregisteredPublisherMessage_ThenPublisherNotRegisteredExceptionIsThrown()
			{
				_mockServiceProvider
					.Setup(_ => _.GetService(typeof(IEnumerable<IBasePublisher>)))
					.Returns(new IBasePublisher[]
					{
						_mockMessageOnePublisher.Object,
						_mockMessageTwoPublisher.Object,
					});

				Func<Task> act = async () => await _sut.PublishAsync(new DummyTestMessageThree());

				act.Should().ThrowAsync<PublisherNotRegisteredException>();
			}
		}
	}
}
